using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public string fileName;
    private char[,] map;

    void Start()
    {
        LoadMap();
        SendMapToManager();
        AdjustCameraPosition();
    }

    void LoadMap()
    {
        fileName = PlayerPrefs.GetString("CurrentStage", "Stage1");
        if (string.IsNullOrEmpty(fileName))
        {
            Debug.LogError("");
            return;
        }

        TextAsset textAsset = Resources.Load<TextAsset>(fileName);
        if (textAsset == null)
        {
            Debug.LogError("");
            return;
        }

        string[] lines = textAsset.text.Split('\n');
        if (lines.Length < 1)
        {
            Debug.LogError("");
            return;
        }

        string[] sizeInfo = lines[0].Trim().Split(' ');
        if (sizeInfo.Length < 2 || !int.TryParse(sizeInfo[0], out int rows) || !int.TryParse(sizeInfo[1], out int cols))
        {
            Debug.LogError("");
            return;
        }

        map = new char[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            if (i + 1 < lines.Length)
            {
                string line = lines[i + 1].Trim();
                int length = Mathf.Min(line.Length, cols);

                for (int j = 0; j < cols; j++)
                {
                    map[i, j] = (j < length) ? line[j] : '.';
                }
            }
            else
            {
                for (int j = 0; j < cols; j++)
                {
                    map[i, j] = '.';
                }
            }
        }
    }

    void SendMapToManager()
    {
        MapManager mapManager = FindFirstObjectByType<MapManager>();
        if (mapManager != null)
        {
            mapManager.SetMapData(map);
        }
        else
        {
            Debug.LogError("");
        }
    }

    void AdjustCameraPosition()
    {
        if (map == null) return;

        int cols = map.GetLength(0);
        int rows = map.GetLength(1);

        float xPos = (cols - 1) / 2f;
        float zPos = (rows - 1) / 2f;

        Camera mainCamera = Camera.main;
        if (mainCamera == null) return;

        float aspectRatio = (float)Screen.width / Screen.height;
        float fovInRadians = mainCamera.fieldOfView * Mathf.Deg2Rad;

        float mapWidth = cols;
        float mapHeight = rows;

        float requiredHeightForWidth = (mapWidth / 2f) / Mathf.Tan(fovInRadians / 2f);
        float requiredHeightForHeight = (mapHeight / (2f * aspectRatio)) / Mathf.Tan(fovInRadians / 2f);

        float yPos = Mathf.Max(requiredHeightForWidth, requiredHeightForHeight);

        mainCamera.transform.position = new Vector3(xPos, yPos, zPos);
    }
}

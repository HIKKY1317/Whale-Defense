using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public string fileName;
    private char[,] map;
    private MapManager mapManager;
    private CameraController cameraController;

    void Start()
    {
        mapManager = FindFirstObjectByType<MapManager>();
        cameraController = FindFirstObjectByType<CameraController>();
        LoadMap();
    }

    void LoadMap()
    {
        fileName = PlayerPrefs.GetString("CurrentStage", "Stage/Stage1");
        if (string.IsNullOrEmpty(fileName))
        {
            Debug.LogError("Map file name is empty.");
            return;
        }

        TextAsset textAsset = Resources.Load<TextAsset>(fileName);
        if (textAsset == null)
        {
            Debug.LogError("Failed to load the map file.");
            return;
        }

        string[] lines = textAsset.text.Split('\n');
        if (lines.Length < 1)
        {
            Debug.LogError("Map file is empty.");
            return;
        }

        string[] sizeInfo = lines[0].Trim().Split(' ');
        if (sizeInfo.Length < 2 || !int.TryParse(sizeInfo[0], out int cols) || !int.TryParse(sizeInfo[1], out int rows))
        {
            Debug.LogError("Invalid map size information.");
            return;
        }

        map = new char[cols, rows];

        for (int i = 0; i < cols; i++)
        {
            if (i + 1 < lines.Length)
            {
                string line = lines[i + 1].Trim();
                int length = Mathf.Min(line.Length, rows);

                for (int j = 0; j < rows; j++)
                {
                    map[i, j] = (j < length) ? line[j] : '.';
                }
            }
            else
            {
                for (int j = 0; j < rows; j++)
                {
                    map[i, j] = '.';
                }
            }
        }

        if (mapManager != null)
        {
            mapManager.SetMapData(map);
        }
        else
        {
            Debug.LogError("MapManager not found.");
        }

        if (cameraController != null)
        {
            cameraController.SetCameraPosition(map);
        }
        else
        {
            Debug.LogError("CameraController not found.");
        }
    }
}

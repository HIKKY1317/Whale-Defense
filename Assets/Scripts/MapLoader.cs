using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public string fileName;
    private char[,] map = new char[10, 10];

    void Start()
    {
        LoadMap();
        SendMapToManager();
    }

    void LoadMap()
    {
        if (string.IsNullOrEmpty(fileName))
        {
            Debug.LogError("�t�@�C�������ݒ肳��Ă��܂���");
            return;
        }

        TextAsset textAsset = Resources.Load<TextAsset>(fileName);
        if (textAsset == null)
        {
            Debug.LogError("�t�@�C����������܂���: " + fileName);
            return;
        }

        string[] lines = textAsset.text.Split('\n');

        for (int i = 0; i < 10; i++)
        {
            if (i < lines.Length)
            {
                string line = lines[i].Trim();
                int length = Mathf.Min(line.Length, 10);

                for (int j = 0; j < 10; j++)
                {
                    map[i, j] = (j < length) ? line[j] : '.';
                }
            }
            else
            {
                for (int j = 0; j < 10; j++)
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
            Debug.LogError("MapManager��������܂���B�V�[���ɔz�u���Ă��������B");
        }
    }
}

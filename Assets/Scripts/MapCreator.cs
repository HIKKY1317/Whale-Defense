using UnityEngine;

public class MapCreator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject housePrefab;
    private char[,] map;

    public void SetMapData(char[,] receivedMap)
    {
        map = receivedMap;
        GenerateMap();
    }

    void GenerateMap()
    {
        if (map == null)
        {
            Debug.LogError("マップデータが設定されていません。");
            return;
        }

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (map[i, j] == '#')
                {
                    Vector3 position = new Vector3(i, 0, j);
                    Instantiate(wallPrefab, position, Quaternion.identity);
                }
                if (map[i, j] == 'H')
                {
                    Vector3 position = new Vector3(i, 0, j);
                    Instantiate(housePrefab, position, Quaternion.identity);
                }
            }
        }
    }
}

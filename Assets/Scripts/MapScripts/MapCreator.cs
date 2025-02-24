using UnityEngine;
using System.Collections.Generic;

public class MapCreator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject housePrefab;
    public GameObject spawnerPrefab;
    public GameObject placementPrefab;
    public GameObject turretPrefab;

    private char[,] map;
    private GameObject mapContainer;
    private MapPathChecker mapPathChecker;

    private Dictionary<Vector2Int, GameObject> preservedObjects = new Dictionary<Vector2Int, GameObject>();

    public void SetMapData(char[,] receivedMap)
    {
        map = receivedMap;
        GenerateMap();
    }

    void GenerateMap()
    {
        int rows = map.GetLength(0);
        int cols = map.GetLength(1);
        mapPathChecker = FindFirstObjectByType<MapPathChecker>();
        if (mapPathChecker == null)
        {
            Debug.LogError("MapPathChecker not found.");
        }

        if (map == null)
        {
            Debug.LogError("Map data is null.");
            return;
        }

        ResetMap();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Vector2Int positionKey = new Vector2Int(i, j);
                Vector3 position = new Vector3(i, 0, j);
                GameObject obj = null;

                if (map[i, j] == '#')
                {
                    obj = Instantiate(wallPrefab, position, Quaternion.identity);
                }
                else if (map[i, j] == 'H' || map[i, j] == 'S' || map[i, j] == 'T')
                {
                    if (!preservedObjects.ContainsKey(positionKey))
                    {
                        obj = Instantiate(map[i, j] == 'H' ? housePrefab : map[i, j] == 'S' ? spawnerPrefab : turretPrefab, position, Quaternion.identity);
                        preservedObjects[positionKey] = obj;
                    }
                    else
                    {
                        obj = preservedObjects[positionKey];
                    }
                }
                else if (map[i, j] == '.')
                {
                    if (preservedObjects.ContainsKey(positionKey))
                    {
                        Destroy(preservedObjects[positionKey]);
                        preservedObjects.Remove(positionKey);
                    }

                    if (!mapPathChecker.BlocksPath(i, j))
                    {
                        obj = Instantiate(placementPrefab, position, Quaternion.identity);
                    }
                }

                if (obj != null)
                {
                    obj.transform.parent = mapContainer.transform;
                }
            }
        }
    }

    void ResetMap()
    {
        if (mapContainer == null)
        {
            mapContainer = new GameObject("MapContainer");
            return;
        }

        List<Transform> childrenToDestroy = new List<Transform>();

        foreach (Transform child in mapContainer.transform)
        {
            Vector2Int positionKey = new Vector2Int((int)child.position.x, (int)child.position.z);

            if (!preservedObjects.ContainsKey(positionKey) && child.gameObject.tag != "TurretBase")
            {
                childrenToDestroy.Add(child);
            }
        }

        foreach (var child in childrenToDestroy)
        {
            Destroy(child.gameObject);
        }
    }
}
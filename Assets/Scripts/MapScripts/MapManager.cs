using UnityEngine;

public class MapManager : MonoBehaviour
{
    private char[,] map;

    public void SetMapData(char[,] loadedMap)
    {
        map = loadedMap;
        SendMapToPathChecker();
        SendMapToCreator();
        SendMapToRouteCalculator();
    }

    void SendMapToPathChecker()
    {
        MapPathChecker mapPathChecker = FindFirstObjectByType<MapPathChecker>();
        if (mapPathChecker != null)
        {
            mapPathChecker.SetMapData(map);
        }
        else
        {
            Debug.LogError("MapPathChecker not found. Please ensure the MapPathChecker component is present in the scene.");
        }
    }

    void SendMapToCreator()
    {
        MapCreator mapCreator = FindFirstObjectByType<MapCreator>();
        if (mapCreator != null)
        {
            mapCreator.SetMapData(map);
        }
        else
        {
            Debug.LogError("MapCreator not found. Please ensure the MapCreator component is present in the scene.");
        }
    }

    void SendMapToRouteCalculator()
    {
        MapRouteCalculator mapRouteCalculator = FindFirstObjectByType<MapRouteCalculator>();
        if (mapRouteCalculator != null)
        {
            mapRouteCalculator.SetMapData(map);
        }
        else
        {
            Debug.LogError("MapRouteCalculator not found. Please ensure the MapRouteCalculator component is present in the scene.");
        }
    }

    public char[,] GetMapData()
    {
        return map;
    }
}

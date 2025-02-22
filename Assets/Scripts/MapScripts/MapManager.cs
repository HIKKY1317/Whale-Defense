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
            Debug.LogError("MapPathCheckerが見つかりません。シーンに配置してください。");
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
            Debug.LogError("MapCreatorが見つかりません。シーンに配置してください。");
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
            Debug.LogError("MapRouteCalculatorが見つかりません。シーンに配置してください。");
        }
    }

    public char[,] GetMapData()
    {
        return map;
    }

}

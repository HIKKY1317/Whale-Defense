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
            Debug.LogError("MapPathChecker��������܂���B�V�[���ɔz�u���Ă��������B");
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
            Debug.LogError("MapCreator��������܂���B�V�[���ɔz�u���Ă��������B");
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
            Debug.LogError("MapRouteCalculator��������܂���B�V�[���ɔz�u���Ă��������B");
        }
    }

    public char[,] GetMapData()
    {
        return map;
    }

}

using UnityEngine;

public class MapManager : MonoBehaviour
{
    private char[,] map;

    public void SetMapData(char[,] loadedMap)
    {
        map = loadedMap;
        SendMapToCreator();
        SendMapToRouteCalculator();
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
}

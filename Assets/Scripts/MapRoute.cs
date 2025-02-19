using System.Collections.Generic;
using UnityEngine;

public class MapRoute : MonoBehaviour
{
    public GameObject routePrefab;

    private List<Vector2Int> route;
    private List<GameObject> instantiatedRoutes = new List<GameObject>();

    public void SetRoute(List<Vector2Int> receivedRoute)
    {
        ClearOldRoute();
        route = receivedRoute;
        DisplayRoute();
    }

    private void DisplayRoute()
    {
        if (routePrefab == null)
        {
            Debug.LogError("Routeプレハブが設定されていません。");
            return;
        }

        foreach (var point in route)
        {
            Vector3 position = new Vector3(point.x, 0, point.y);
            GameObject routeObj = Instantiate(routePrefab, position, Quaternion.identity);
            instantiatedRoutes.Add(routeObj);
        }
    }

    private void ClearOldRoute()
    {
        foreach (var obj in instantiatedRoutes)
        {
            Destroy(obj);
        }
        instantiatedRoutes.Clear();
    }
}

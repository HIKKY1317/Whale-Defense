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
    }

    private void ClearOldRoute()
    {
        foreach (var obj in instantiatedRoutes)
        {
            Destroy(obj);
        }
        instantiatedRoutes.Clear();
    }

    public List<Vector2Int> GetRoute()
    {
        return route;
    }
}

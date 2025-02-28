using UnityEngine;

public class MapController : MonoBehaviour
{
    private MapManager mapManager;
    private TurretDragDrop turretDragDrop;

    void Start()
    {
        mapManager = FindFirstObjectByType<MapManager>();
        if (mapManager == null)
        {
            Debug.LogError("MapManager not found");
        }

        turretDragDrop = FindFirstObjectByType<TurretDragDrop>();
    }

    public void SetMap(Vector2Int gridPos)
    {
        char[,] map = mapManager.GetMapData();
        map[gridPos.x, gridPos.y] = '.';
        mapManager.SetMapData(map);
    }

    public Vector2Int GetGridPosition(Transform turretTransform)
    {
        if (turretDragDrop != null && turretDragDrop.GetSelectedTurret() == turretTransform.gameObject)
        {
            Vector3 initPos = turretDragDrop.GetInitialPosition();
            return new Vector2Int(
                Mathf.FloorToInt(initPos.x),
                Mathf.FloorToInt(initPos.z)
            );
        }

        return new Vector2Int(
            Mathf.FloorToInt(turretTransform.position.x),
            Mathf.FloorToInt(turretTransform.position.z)
        );
    }
}

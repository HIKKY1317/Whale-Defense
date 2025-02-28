using UnityEngine;

public class MapPlacement : MonoBehaviour
{
    private MapManager mapManager;
    private MapPathChecker mapPathChecker;
    private MoneyManager moneyManager;
    private TurretPrefabSelector turretPrefabSelector;

    void Start()
    {
        mapPathChecker = FindFirstObjectByType<MapPathChecker>();
        if (mapPathChecker == null)
        {
            Debug.LogError("MapPathChecker not found in the scene.");
        }

        mapManager = FindFirstObjectByType<MapManager>();
        if (mapManager == null)
        {
            Debug.LogError("MapManager not found in the scene.");
        }

        moneyManager = FindFirstObjectByType<MoneyManager>();
        if (moneyManager == null)
        {
            Debug.LogError("MoneyManager not found in the scene.");
        }

        turretPrefabSelector = FindFirstObjectByType<TurretPrefabSelector>();
        if (turretPrefabSelector == null)
        {
            Debug.LogError("TurretPrefabSelector not found in the scene.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 hitPosition = hit.point;
                int h = Mathf.RoundToInt(hitPosition.x);
                int w = Mathf.RoundToInt(hitPosition.z);
                UpdateMapData(h, w);
            }
        }
    }

    void UpdateMapData(int h, int w)
    {
        if (mapManager != null && turretPrefabSelector != null)
        {
            char[,] map = mapManager.GetMapData();

            if (map == null)
            {
                Debug.LogError("Map data is null.");
                return;
            }

            if (h >= 0 && h < map.GetLength(0) && w >= 0 && w < map.GetLength(1))
            {
                if (map[h, w] == '.')
                {
                    if (!mapPathChecker.BlocksPath(h, w))
                    {
                        int turretCost = turretPrefabSelector.selectedTurretCost;
                        if (moneyManager.CanAfford(turretCost))
                        {
                            map[h, w] = 'T';
                            mapManager.SetMapData(map);
                            moneyManager.SpendMoney(turretCost);
                            Debug.Log("Turret placed successfully.");
                        }
                        else
                        {
                            Debug.Log("Not enough money to place the turret.");
                        }
                    }
                    else
                    {
                        Debug.Log("Cannot place turret as it blocks the path.");
                    }
                }
                else
                {
                    Debug.Log("Cannot place turret on this tile.");
                }
            }
            else
            {
                Debug.Log("Coordinates out of map bounds.");
            }
        }
    }
}

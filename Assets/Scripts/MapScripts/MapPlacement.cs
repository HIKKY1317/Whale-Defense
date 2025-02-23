using UnityEngine;

public class MapPlacement : MonoBehaviour
{
    private MapManager mapManager;
    private MapPathChecker mapPathChecker;
    private PlayerAttributes playerAttributes;
    private TarotPrefabSelector tarotPrefabSelector;

    void Start()
    {
        mapPathChecker = FindFirstObjectByType<MapPathChecker>();
        if (mapPathChecker == null)
        {
            Debug.LogError("");
        }

        mapManager = FindFirstObjectByType<MapManager>();
        if (mapManager == null)
        {
            Debug.LogError("");
        }

        playerAttributes = FindFirstObjectByType<PlayerAttributes>();
        if (playerAttributes == null)
        {
            Debug.LogError("");
        }

        tarotPrefabSelector = FindFirstObjectByType<TarotPrefabSelector>();
        if (tarotPrefabSelector == null)
        {
            Debug.LogError("");
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
                int x = Mathf.RoundToInt(hitPosition.x);
                int z = Mathf.RoundToInt(hitPosition.z);
                UpdateMapData(x, z);
            }
        }
    }

    void UpdateMapData(int x, int z)
    {
        if (mapManager != null && tarotPrefabSelector != null)
        {
            char[,] map = mapManager.GetMapData();

            if (map == null)
            {
                Debug.LogError("");
                return;
            }

            if (x >= 0 && x < map.GetLength(0) && z >= 0 && z < map.GetLength(1))
            {
                if (map[x, z] == '.')
                {
                    if (!mapPathChecker.BlocksPath(x, z))
                    {
                        int tarotCost = tarotPrefabSelector.selectedTarotCost;
                        if (playerAttributes.CanAfford(tarotCost))
                        {
                            map[x, z] = 'T';
                            mapManager.SetMapData(map);
                            playerAttributes.SpendMoney(tarotCost);
                            Debug.Log("");
                        }
                        else
                        {
                            Debug.Log("");
                        }
                    }
                    else
                    {
                        Debug.Log("");
                    }
                }
                else
                {
                    Debug.Log("");
                }
            }
            else
            {
                Debug.LogWarning("");
            }
        }
    }
}

using UnityEngine;

public class MapPlacement : MonoBehaviour
{
    private MapManager mapManager;

    private MapPathChecker mapPathChecker;
    private PlayerAttributes playerAttributes;

    void Start()
    {
        mapPathChecker = FindFirstObjectByType<MapPathChecker>();
        if (mapPathChecker == null)
        {
            Debug.LogError("MapPathCheckerが見つかりません。シーンに配置してください。");
        }

        mapManager = FindFirstObjectByType<MapManager>();
        if (mapManager == null)
        {
            Debug.LogError("MapManagerが見つかりません。シーンに配置してください。");
        }

        playerAttributes = FindFirstObjectByType<PlayerAttributes>();
        if (playerAttributes == null)
        {
            Debug.LogError("PlayerAttributesが見つかりません。シーンに配置してください。");
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
        if (mapManager != null)
        {
            char[,] map = mapManager.GetMapData();

            if (map == null)
            {
                Debug.LogError("Mapデータが設定されていません。");
                return;
            }

            if (x >= 0 && x < map.GetLength(0) && z >= 0 && z < map.GetLength(1))
            {
                if (map[x, z] == '.')
                {
                    if (!mapPathChecker.BlocksPath(x, z))
                    {
                        if (playerAttributes.CanAfford(20))
                        {
                            map[x, z] = 'T';
                            mapManager.SetMapData(map);
                            playerAttributes.SpendMoney(20);
                            Debug.Log($"Map updated at ({x}, {z})");
                        }
                        else
                        {
                            Debug.Log("お金が足りません");
                        }
                    }
                    else
                    {
                        Debug.Log($"座標 ({x}, {z}) は変更できません（現在: {map[x, z]}）");
                    }
                }
                else
                {
                    Debug.Log($"座標 ({x}, {z}) は変更できません（現在: {map[x, z]}）");
                }
            }
            else
            {
                Debug.LogWarning($"座標 ({x}, {z}) はマップ範囲外です。");
            }
        }
    }
}

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
            Debug.LogError("MapPathChecker��������܂���B�V�[���ɔz�u���Ă��������B");
        }

        mapManager = FindFirstObjectByType<MapManager>();
        if (mapManager == null)
        {
            Debug.LogError("MapManager��������܂���B�V�[���ɔz�u���Ă��������B");
        }

        playerAttributes = FindFirstObjectByType<PlayerAttributes>();
        if (playerAttributes == null)
        {
            Debug.LogError("PlayerAttributes��������܂���B�V�[���ɔz�u���Ă��������B");
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
                Debug.LogError("Map�f�[�^���ݒ肳��Ă��܂���B");
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
                            Debug.Log("����������܂���");
                        }
                    }
                    else
                    {
                        Debug.Log($"���W ({x}, {z}) �͕ύX�ł��܂���i����: {map[x, z]}�j");
                    }
                }
                else
                {
                    Debug.Log($"���W ({x}, {z}) �͕ύX�ł��܂���i����: {map[x, z]}�j");
                }
            }
            else
            {
                Debug.LogWarning($"���W ({x}, {z}) �̓}�b�v�͈͊O�ł��B");
            }
        }
    }
}

using UnityEngine;

public class TurretDragDrop : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject selectedTurret;
    private Vector3 initialPosition;
    private GameObject existingTurret;
    private Vector3 existingPosition;
    private MapManager mapManager;
    private MapCreator mapCreator;
    private TurretRecipeManager turretRecipeManager;

    public GameObject mergedTurretPrefab;

    void Start()
    {
        mainCamera = Camera.main;
        mapManager = FindFirstObjectByType<MapManager>();
        if (mapManager == null)
        {
            Debug.LogError("MapManager not found.");
        }

        turretRecipeManager = FindFirstObjectByType<TurretRecipeManager>();
        if (turretRecipeManager == null)
        {
            Debug.LogError("TurretRecipeManager not found.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 pos = hit.collider.transform.position;
                int x = Mathf.RoundToInt(pos.x);
                int z = Mathf.RoundToInt(pos.z);
                
                if (IsWithinBounds(x, z) && mapManager.GetMapData()[x, z] == 'T')
                {
                    selectedTurret = hit.collider.transform.parent.gameObject;
                    initialPosition = selectedTurret.transform.position;
                    SetLayerRecursively(selectedTurret, "DraggedTurret");
                }
            }
        }

        if (selectedTurret != null && Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                selectedTurret.transform.position = new Vector3(hit.point.x, initialPosition.y, hit.point.z);
            }
        }

        if (selectedTurret != null && Input.GetMouseButtonUp(0))
        {

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            int layerMask = ~(1 << LayerMask.NameToLayer("DraggedTurret"));
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                Vector3 pos = hit.collider.transform.position;
                int newX = Mathf.RoundToInt(pos.x);
                int newZ = Mathf.RoundToInt(pos.z);
                int oldX = Mathf.RoundToInt(initialPosition.x);
                int oldZ = Mathf.RoundToInt(initialPosition.z);

                if (IsWithinBounds(newX, newZ) && mapManager.GetMapData()[newX, newZ] == 'T')
                {
                    existingTurret = hit.collider.transform.parent.gameObject;
                    existingPosition = existingTurret.transform.position;
                }

                char[,] map = mapManager.GetMapData();

                if (!IsWithinBounds(newX, newZ) || !IsWithinBounds(oldX, oldZ))
                {
                    selectedTurret.transform.position = initialPosition;
                    SetLayerRecursively(selectedTurret, "Default");
                    selectedTurret = null;
                    return;
                }

                if (map[newX, newZ] == 'T' && (newX != oldX || newZ != oldZ))
                {
                    if (existingTurret != null)
                    {
                        if (turretRecipeManager.GetMergedTurret(existingTurret, selectedTurret) != null)
                        {
                            mergedTurretPrefab = turretRecipeManager.GetMergedTurret(existingTurret, selectedTurret);
                            map[oldX, oldZ] = '.';
                            GameObject newTurret = Instantiate(mergedTurretPrefab, new Vector3(newX, initialPosition.y, newZ), Quaternion.identity);
                            Destroy(existingTurret);
                            Destroy(selectedTurret);
                            mapManager.SetMapData(map);
                        }
                        else
                        {
                            selectedTurret.transform.position = initialPosition;
                            SetLayerRecursively(selectedTurret, "Default");
                            selectedTurret = null;
                            return;
                        }
                    }
                    else
                    {
                        selectedTurret.transform.position = initialPosition;
                        SetLayerRecursively(selectedTurret, "Default");
                        selectedTurret = null;
                        return;
                    }
                }
                else
                {
                    selectedTurret.transform.position = initialPosition;
                    SetLayerRecursively(selectedTurret, "Default");
                    selectedTurret = null;
                    return;
                }
            }
        }
    }

    bool IsWithinBounds(int x, int z)
    {
        char[,] map = mapManager.GetMapData();
        return x >= 0 && x < map.GetLength(0) && z >= 0 && z < map.GetLength(1);
    }

    void SetLayerRecursively(GameObject obj, string layerName)
    {
        obj.layer = LayerMask.NameToLayer(layerName);

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layerName);
        }
    }

    public GameObject GetSelectedTurret()
    {
        return selectedTurret;
    }

    public Vector3 GetInitialPosition()
    {
        return initialPosition;
    }
}

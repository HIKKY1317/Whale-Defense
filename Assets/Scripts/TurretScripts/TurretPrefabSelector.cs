using UnityEngine;

public class TurretPrefabSelector : MonoBehaviour
{
    public GameObject miniTurretPrefab;
    public GameObject turretPrefab;
    public GameObject largeTurretPrefab;

    private MapCreator mapCreator;

    public int miniTurretCost = 20;
    public int turretCost = 40;
    public int largeTurretCost = 60;

    public GameObject selectedTurretPrefab { get; private set; }
    public int selectedTurretCost { get; private set; }

    void Start()
    {
        mapCreator = FindFirstObjectByType<MapCreator>();
        if (mapCreator == null)
        {
            Debug.LogError("mapCreator not Found");
        }

        SelectTurret();
    }

    public void SelectMiniTurret()
    {
        if (mapCreator != null)
        {
            mapCreator.turretPrefab = miniTurretPrefab;
            selectedTurretPrefab = miniTurretPrefab;
            selectedTurretCost = miniTurretCost;
        }
    }

    public void SelectTurret()
    {
        if (mapCreator != null)
        {
            mapCreator.turretPrefab = turretPrefab;
            selectedTurretPrefab = turretPrefab;
            selectedTurretCost = turretCost;
        }
    }

    public void SelectLargeTurret()
    {
        if (mapCreator != null)
        {
            mapCreator.turretPrefab = largeTurretPrefab;
            selectedTurretPrefab = largeTurretPrefab;
            selectedTurretCost = largeTurretCost;
        }
    }
}

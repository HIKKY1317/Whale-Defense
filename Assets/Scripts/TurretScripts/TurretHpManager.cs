using UnityEngine;

public class TurretHpManager : MonoBehaviour
{
    private TurretAttributes turretAttributes;
    private MapController mapController;

    void Start()
    {
        turretAttributes = GetComponent<TurretAttributes>();
        if (turretAttributes == null)
        {
            Debug.LogError("TurretAttributes not found");
        }

        mapController = FindFirstObjectByType<MapController>();
        if (mapController == null)
        {
            Debug.LogError("MapController not found");
        }
    }

    public void TakeDamage(int damage)
    {
        int hp = turretAttributes.GetHp();
        hp -= damage;
        turretAttributes.SetHp(hp);

        if (hp <= 0)
        {
            if (transform.parent != null)
            {
                Vector2Int gridPos = mapController.GetGridPosition(transform.parent);
                mapController.SetMap(gridPos);
                Destroy(transform.parent.gameObject);
            }
        }
    }
}

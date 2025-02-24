using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttributes : MonoBehaviour
{
    public int hp = 100;
    public float attackSpeed = 1.0f;
    public int attackPower = 10;
    public float attackRange = 5.0f;

    private float attackCooldown = 0f;
    private MapManager mapManager;
    void Start()
    {
        mapManager = FindFirstObjectByType<MapManager>();
        if (mapManager == null)
        {
            Debug.LogError("");
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            if (transform.parent != null)
            {
                char[,] map = mapManager.GetMapData();
                Vector2Int gridPos = GetGridPosition();
                map[gridPos.x, gridPos.y] = '.';
                mapManager.SetMapData(map);
                Destroy(transform.parent.gameObject);
            }
        }
    }


    public void UpdateAttackCooldown(float deltaTime)
    {
        attackCooldown -= deltaTime;
    }

    public bool CanAttack()
    {
        return attackCooldown <= 0f;
    }

    public void ResetAttackCooldown()
    {
        attackCooldown = 1f / attackSpeed;
    }

    private Vector2Int GetGridPosition()
    {
        TurretDragDrop turretDragDrop = FindFirstObjectByType<TurretDragDrop>();

        if (turretDragDrop != null && turretDragDrop.GetSelectedTurret() == transform.parent.gameObject)
        {
            Vector3 initPos = turretDragDrop.GetInitialPosition();
            return new Vector2Int(
                Mathf.FloorToInt(initPos.x),
                Mathf.FloorToInt(initPos.z)
            );
        }

        return new Vector2Int(
            Mathf.FloorToInt(transform.position.x),
            Mathf.FloorToInt(transform.position.z)
        );
    }
}

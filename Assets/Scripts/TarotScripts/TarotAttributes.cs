using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarotAttributes : MonoBehaviour
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
            Debug.LogError("MapManagerが見つかりません。シーンに配置してください。");
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
            else
            {
                char[,] map = mapManager.GetMapData();
                Vector2Int gridPos = GetGridPosition();
                map[gridPos.x, gridPos.y] = '.';
                mapManager.SetMapData(map);
                Destroy(gameObject);
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
        int x = Mathf.FloorToInt(transform.position.x);
        int z = Mathf.FloorToInt(transform.position.z);
        return new Vector2Int(x, z);
    }
}

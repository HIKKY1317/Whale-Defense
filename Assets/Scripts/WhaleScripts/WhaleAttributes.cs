using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleAttributes : MonoBehaviour
{
    public string whaleName = "Whale";
    public int maxHp = 200;
    public int hp = 200;
    public float swimSpeed = 2.0f;
    public int attackPower = 5;
    public float attackRange = 1.0f;
    public float attackSpeed = 5.0f;
    public int rewardMoney = 50;

    private float attackCooldown = 0f;

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            PlayerAttributes player = FindFirstObjectByType<PlayerAttributes>();
            if (player != null)
            {
                player.AddMoney(rewardMoney);
            }
            Destroy(gameObject);
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
}

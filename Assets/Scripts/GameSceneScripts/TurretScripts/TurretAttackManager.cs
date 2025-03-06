using UnityEngine;

public class TurretAttackManager : MonoBehaviour
{
    private float attackCooldown = 0f;
    private TurretAttributes turretAttributes;

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
        if (turretAttributes == null)
        {
            turretAttributes = this.gameObject.GetComponent<TurretAttributes>();
        }
        attackCooldown = 1f / turretAttributes.GetAttackSpeed();
    }
}
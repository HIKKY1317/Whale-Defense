using UnityEngine;

public class WhaleAttackManager : MonoBehaviour
{
    private float attackCooldown = 0f;
    private WhaleAttributes whaleAttributes;

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
        if (whaleAttributes == null)
        {
            whaleAttributes = this.gameObject.GetComponent<WhaleAttributes>();
        }
        attackCooldown = 1f / whaleAttributes.GetAttackSpeed();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public GameObject bulletPrefab;

    void Update()
    {
        foreach (TurretAttackManager turretAttackManager in Object.FindObjectsByType<TurretAttackManager>(FindObjectsSortMode.None))
        {
            turretAttackManager.UpdateAttackCooldown(Time.deltaTime);

            if (turretAttackManager.CanAttack())
            {
                TurretAttributes turretAttributes = turretAttackManager.gameObject.GetComponent<TurretAttributes>();
                GameObject target = FindNearestTarget(turretAttributes);
                if (target != null)
                {
                    RotateTowardsTarget(turretAttributes, target);
                    Shoot(turretAttributes, target);
                    turretAttackManager.ResetAttackCooldown();
                }
            }
        }
    }

    private GameObject FindNearestTarget(TurretAttributes turretAttributes)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Whale");
        GameObject nearest = null;
        float minDistance = turretAttributes.GetAttackRange();

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(turretAttributes.gameObject.transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = enemy;
            }
        }
        return nearest;
    }

    private void RotateTowardsTarget(TurretAttributes turretAttributes, GameObject target)
    {
        Transform turretTransform = turretAttributes.gameObject.transform.parent;
        if (turretTransform == null) return;

        Vector3 direction = target.transform.position - turretTransform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            turretTransform.rotation = targetRotation;
        }
    }

    private void Shoot(TurretAttributes turretAttributes, GameObject target)
    {
        Animator animator = turretAttributes.GetAnimator();
        if (animator != null)
        {
            animator.SetTrigger("Shoot");
        }
        GameObject bullet = Instantiate(bulletPrefab, turretAttributes.gameObject.transform.position, Quaternion.identity);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        if (bulletController != null)
        {
            bulletController.SetTarget(target, turretAttributes.GetAttackPower());
        }
    }
}

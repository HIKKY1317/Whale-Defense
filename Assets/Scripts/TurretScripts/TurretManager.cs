using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public GameObject bulletPrefab;

    void Update()
    {
        foreach (TurretAttributes turret in Object.FindObjectsByType<TurretAttributes>(FindObjectsSortMode.None))
        {
            turret.UpdateAttackCooldown(Time.deltaTime);

            if (turret.CanAttack())
            {
                GameObject target = FindNearestTarget(turret);
                if (target != null)
                {
                    Shoot(turret, target);
                    turret.ResetAttackCooldown();
                }
            }
        }
    }

    GameObject FindNearestTarget(TurretAttributes turret)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Whale");
        GameObject nearest = null;
        float minDistance = turret.attackRange;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(turret.transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = enemy;
            }
        }
        return nearest;
    }

    void Shoot(TurretAttributes turret, GameObject target)
    {
        GameObject bullet = Instantiate(bulletPrefab, turret.transform.position, Quaternion.identity);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        if (bulletController != null)
        {
            bulletController.SetTarget(target, turret.attackPower);
        }
    }
}

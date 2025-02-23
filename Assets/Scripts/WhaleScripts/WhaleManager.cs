using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleManager : MonoBehaviour
{
    public GameObject bulletPrefab;

    void Update()
    {
        foreach (WhaleAttributes whale in Object.FindObjectsByType<WhaleAttributes>(FindObjectsSortMode.None))
        {
            whale.UpdateAttackCooldown(Time.deltaTime);

            if (whale.CanAttack())
            {
                GameObject target = FindNearestTarget(whale);
                if (target != null)
                {
                    Shoot(whale, target);
                    whale.ResetAttackCooldown();
                }
            }
        }
    }

    GameObject FindNearestTarget(WhaleAttributes whale)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Turret");
        GameObject nearest = null;
        float minDistance = whale.attackRange;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(whale.transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = enemy;
            }
        }
        return nearest;
    }

    void Shoot(WhaleAttributes whale, GameObject target)
    {
        GameObject bullet = Instantiate(bulletPrefab, whale.transform.position, Quaternion.identity);
        WhaleBulletController whaleBulletController = bullet.GetComponent<WhaleBulletController>();
        if (whaleBulletController != null)
        {
            whaleBulletController.SetTarget(target, whale.attackPower);
        }
    }
}

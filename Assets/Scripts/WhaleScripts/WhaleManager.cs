using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleManager : MonoBehaviour
{
    public GameObject bulletPrefab;

    void Update()
    {
        foreach (WhaleAttackManager whaleAttackManager in Object.FindObjectsByType<WhaleAttackManager>(FindObjectsSortMode.None))
        {
            whaleAttackManager.UpdateAttackCooldown(Time.deltaTime);

            if (whaleAttackManager.CanAttack())
            {
                WhaleAttributes whaleAttributes = whaleAttackManager.gameObject.GetComponent<WhaleAttributes>();
                GameObject target = FindNearestTarget(whaleAttributes);
                if (target != null)
                {
                    Shoot(whaleAttributes, target);
                    whaleAttackManager.ResetAttackCooldown();
                }
            }
        }
    }

    private GameObject FindNearestTarget(WhaleAttributes whaleAttributes)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Turret");
        GameObject nearest = null;
        float minDistance = whaleAttributes.GetAttackRange();

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(whaleAttributes.gameObject.transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = enemy;
            }
        }
        return nearest;
    }

    void Shoot(WhaleAttributes whaleAttributes, GameObject target)
    {
        GameObject bullet = Instantiate(bulletPrefab, whaleAttributes.gameObject.transform.position, Quaternion.identity);
        WhaleBulletController whaleBulletController = bullet.GetComponent<WhaleBulletController>();
        if (whaleBulletController != null)
        {
            whaleBulletController.SetTarget(target, whaleAttributes.GetAttackPower());
        }
    }
}

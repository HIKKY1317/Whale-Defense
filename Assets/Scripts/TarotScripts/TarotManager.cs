using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarotManager : MonoBehaviour
{
    public GameObject bulletPrefab;

    void Update()
    {
        foreach (TarotAttributes tarot in Object.FindObjectsByType<TarotAttributes>(FindObjectsSortMode.None))
        {
            tarot.UpdateAttackCooldown(Time.deltaTime);

            if (tarot.CanAttack())
            {
                GameObject target = FindNearestTarget(tarot);
                if (target != null)
                {
                    Shoot(tarot, target);
                    tarot.ResetAttackCooldown();
                }
            }
        }
    }

    GameObject FindNearestTarget(TarotAttributes tarot)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Whale");
        GameObject nearest = null;
        float minDistance = tarot.attackRange;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(tarot.transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = enemy;
            }
        }
        return nearest;
    }

    void Shoot(TarotAttributes tarot, GameObject target)
    {
        GameObject bullet = Instantiate(bulletPrefab, tarot.transform.position, Quaternion.identity);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        if (bulletController != null)
        {
            bulletController.SetTarget(target, tarot.attackPower);
        }
    }
}

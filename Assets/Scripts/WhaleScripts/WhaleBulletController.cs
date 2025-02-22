using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleBulletController : MonoBehaviour
{
    private GameObject target;
    private int attackPower;
    private float speed = 10f;

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
            {
                DealDamage();
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(GameObject newTarget, int power)
    {
        target = newTarget;
        attackPower = power;
    }

    void DealDamage()
    {
        if (target != null)
        {
            TarotAttributes tarotAttributes = target.GetComponent<TarotAttributes>();
            if (tarotAttributes != null)
            {
                tarotAttributes.TakeDamage(attackPower);
            }
        }
    }
}

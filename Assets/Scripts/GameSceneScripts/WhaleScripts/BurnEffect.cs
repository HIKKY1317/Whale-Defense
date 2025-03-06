using System.Collections;
using UnityEngine;

public class BurnEffect : MonoBehaviour
{
    private WhaleAttributes whale;
    private int level;
    private float interval = 0.5f;

    void Start()
    {
        whale = GetComponent<WhaleAttributes>();
        if (whale != null)
        {
            level = whale.GetEffectLevel("Burn");
            StartCoroutine(ApplyBurnEffect());
        }
    }

    IEnumerator ApplyBurnEffect()
    {
        while (whale.GetEffectLevel("Burn") > 0)
        {
            int damage = (int)Mathf.Pow(10, whale.GetEffectLevel("Burn"));
            whale.hp = Mathf.Max(whale.hp - damage, 0);
            yield return new WaitForSeconds(interval);
        }
    }
}

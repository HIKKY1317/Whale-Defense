using UnityEngine;

public class FreezeEffect : MonoBehaviour
{
    private WhaleAttributes whale;
    private float originalSpeed;

    void Start()
    {
        whale = GetComponent<WhaleAttributes>();
        if (whale != null)
        {
            originalSpeed = whale.swimSpeed;
            ApplyFreezeEffect();
        }
    }

    void ApplyFreezeEffect()
    {
        int level = whale.GetEffectLevel("Freeze");
        float multiplier = level == 1 ? 0.9f : level == 2 ? 0.5f : 0.1f;
        whale.swimSpeed = originalSpeed * multiplier;
    }
}

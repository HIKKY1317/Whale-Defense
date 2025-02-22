using UnityEngine;
using UnityEngine.UI;

public class WhaleHpDisplay : MonoBehaviour
{
    public WhaleAttributes whaleAttributes;
    public Image hpBarBackground;
    public Image hpBarForeground;
    public float heightAboveWhale = 2.0f;
    public Vector2 imageSize = new Vector2(1f, 0.2f);

    void Update()
    {
        if (whaleAttributes != null && hpBarBackground != null && hpBarForeground != null)
        {
            float hpPercentage = (float)whaleAttributes.hp / whaleAttributes.maxHp;

            RectTransform foregroundRectTransform = hpBarForeground.GetComponent<RectTransform>();
            if (foregroundRectTransform != null)
            {
                foregroundRectTransform.sizeDelta = new Vector2(imageSize.x * hpPercentage, imageSize.y);
            }

            RectTransform backgroundRectTransform = hpBarBackground.GetComponent<RectTransform>();
            if (backgroundRectTransform != null)
            {
                backgroundRectTransform.sizeDelta = new Vector2(imageSize.x * (1f - hpPercentage), imageSize.y);
            }

            Vector3 whalePosition = whaleAttributes.transform.position;
            Quaternion whaleRotation = whaleAttributes.transform.rotation;

            Vector3 offsetX = whaleAttributes.transform.right * imageSize.x * (1f - hpPercentage) * 0.5f;

            Vector3 offsetXBg = whaleAttributes.transform.right * imageSize.x * hpPercentage * 0.5f;

            Vector3 offsetY = Vector3.up * heightAboveWhale;

            hpBarForeground.transform.position = whalePosition - offsetX + offsetY;
            hpBarBackground.transform.position = whalePosition + offsetXBg + offsetY;
        }
    }

    void Start()
    {
        if (whaleAttributes == null)
        {
            whaleAttributes = GetComponentInParent<WhaleAttributes>();
        }

        if (hpBarBackground == null || hpBarForeground == null)
        {
            Debug.LogError("HP Bar Image components are not assigned.");
        }
    }
}
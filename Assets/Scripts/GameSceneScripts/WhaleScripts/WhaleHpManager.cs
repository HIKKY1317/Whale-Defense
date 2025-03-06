using UnityEngine;

public class WhaleHpManager : MonoBehaviour
{
    private WhaleAttributes whaleAttributes;

    void Start()
    {
        whaleAttributes = GetComponent<WhaleAttributes>();
        if (whaleAttributes == null)
        {
            Debug.LogError("WhaleAttributes not found");
        }
    }

    public void TakeDamage(int damage)
    {
        int hp = whaleAttributes.GetHp();
        hp -= damage;
        whaleAttributes.SetHp(hp);

        if (hp <= 0)
        {
            MoneyManager moneyManager = FindFirstObjectByType<MoneyManager>();
            if (moneyManager != null)
            {
                moneyManager.AddMoney(whaleAttributes.GetRewardMoney());
            }
            Destroy(gameObject);
        }
    }
}

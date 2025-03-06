using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    private PlayerAttributes playerAttributes;

    void Start()
    {
        playerAttributes = FindFirstObjectByType<PlayerAttributes>();
    }

    public void AddMoney(int amount)
    {
        int money = playerAttributes.GetMoney();
        money += amount;
        playerAttributes.SetMoney(money);
    }

    public void SpendMoney(int amount)
    {
        int money = playerAttributes.GetMoney();
        if (money >= amount)
        {
            playerAttributes.SetMoney(money - amount);
        }
        else
        {
            Debug.Log("Not Enough money!");
        }
    }

    public bool CanAfford(int amount)
    {
        int money = playerAttributes.GetMoney();
        return money >= amount;
    }
}

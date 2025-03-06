using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour
{
    public int money = 100;
    public int hp = 100;
    private GameOverController gameOverController;
    private MoneyManager moneyManager;
    private PlayerHpManager playerHpManager;

    void Start()
    {
        gameOverController = FindFirstObjectByType<GameOverController>();
        moneyManager = FindFirstObjectByType<MoneyManager>();
        playerHpManager = FindFirstObjectByType<PlayerHpManager>();
        money = PlayerPrefs.GetInt("Money", 100);
    }

    public int GetMoney()
    {
        return money;
    }

    public int GetHp()
    {
        return hp;
    }

    public void SetMoney(int money)
    {
        this.money = money;
    }

    public void SetHp(int hp)
    {
        this.hp = hp;
    }
}
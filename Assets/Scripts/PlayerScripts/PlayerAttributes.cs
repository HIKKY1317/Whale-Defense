using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour
{
    public int money = 100;
    public int hp = 100;
    public GameOverController gameOverController;

    void Start()
    {
        gameOverController = FindFirstObjectByType<GameOverController>();
        money = PlayerPrefs.GetInt("Money", 100);
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public void SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
        }
        else
        {
            Debug.Log("");
        }
    }

    public int GetMoney()
    {
        return money;
    }

    public bool CanAfford(int amount)
    {
        return money >= amount;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            gameOverController.GameOver();
        }
    }
}
using UnityEngine;

public class PlayerHpManager : MonoBehaviour
{
    private PlayerAttributes playerAttributes;
    private GameOverController gameOverController;

    void Start()
    {
        playerAttributes = FindFirstObjectByType<PlayerAttributes>();
        gameOverController = FindFirstObjectByType<GameOverController>();
    }

    public void TakeDamage (int damage)
    {
        int hp = playerAttributes.GetHp();
        hp -= damage;
        playerAttributes.SetHp(hp);
        if (hp <= 0)
        {
            hp = 0;
            gameOverController.GameOver();
        }
    }
}

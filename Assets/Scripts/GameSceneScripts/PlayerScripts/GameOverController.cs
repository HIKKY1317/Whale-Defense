using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    private Text gameOverText;

    void Start()
    {
        gameOverText = GameObject.Find("GameOverText").GetComponent<Text>();

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
    }

    public void GameOver()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "Game Over!";
        }
    }
}
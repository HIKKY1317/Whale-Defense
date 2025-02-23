using UnityEngine;
using UnityEngine.UI;

public class DisplayMoney : MonoBehaviour
{
    public PlayerAttributes playerAttributes;
    public Text moneyText;

    void Update()
    {
        if (playerAttributes != null && moneyText != null)
        {
            moneyText.text = "Money: " + playerAttributes.GetMoney();
        }
    }
}
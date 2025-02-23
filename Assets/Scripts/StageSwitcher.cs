using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSwitcher : MonoBehaviour
{
    public Image stageImage;
    public Sprite stage1Sprite;
    public Sprite stage2Sprite;
    public Button nextButton;
    public Button prevButton;
    public Button startButton;
    public Text stageText;
    public Text difficultyText;
    public Text moneyText;

    private int currentStage = 1;
    private int level = 1;
    private int money = 100;
    private int difficulty = 1;

    void Start()
    {
        stageImage.sprite = stage1Sprite;
        nextButton.onClick.AddListener(OnNextButtonClicked);
        prevButton.onClick.AddListener(OnPrevButtonClicked);
        startButton.onClick.AddListener(OnStartButtonClicked);
        UpdateStageInfo();
        UpdateStageText();
        UpdateDifficultyAndMoneyText();
    }

    void OnNextButtonClicked()
    {
        if (currentStage < 2)
        {
            currentStage++;
            UpdateStageImage();
            UpdateStageInfo();
            UpdateStageText();
            UpdateDifficultyAndMoneyText();
        }
        else
        {
            currentStage = 1;
            UpdateStageImage();
            UpdateStageInfo();
            UpdateStageText();
            UpdateDifficultyAndMoneyText();
        }
    }

    void OnPrevButtonClicked()
    {
        if (currentStage > 1)
        {
            currentStage--;
            UpdateStageImage();
            UpdateStageInfo();
            UpdateStageText();
            UpdateDifficultyAndMoneyText();
        }
        else
        {
            currentStage = 2;
            UpdateStageImage();
            UpdateStageInfo();
            UpdateStageText();
            UpdateDifficultyAndMoneyText();
        }
    }

    void UpdateStageImage()
    {
        switch (currentStage)
        {
            case 1:
                stageImage.sprite = stage1Sprite;
                break;
            case 2:
                stageImage.sprite = stage2Sprite;
                break;
        }
    }

    void UpdateStageText()
    {
        stageText.text = "Stage " + currentStage;
    }

    void UpdateDifficultyAndMoneyText()
    {
        difficultyText.text = "Difficulty: " + difficulty;
        moneyText.text = "Money: " + money;
    }

    void UpdateStageInfo()
    {
        switch (currentStage)
        {
            case 1:
                level = 1;
                money = 100;
                difficulty = 1;
                break;
            case 2:
                level = 2;
                money = 80;
                difficulty = 1;
                break;
        }
    }

    void OnStartButtonClicked()
    {
        PlayerPrefs.SetString("CurrentStage", "Stage/Stage" + currentStage);
        PlayerPrefs.SetString("Level", "Level/Level" + level);
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("Difficulty", difficulty);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }
}

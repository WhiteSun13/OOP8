using UnityEngine;
using UnityEngine.UI;

interface ILevelController
{
    void SetPlayerUIText();
    void GameOver();
    void ChangeLevel();
}

public class LevelController : MonoBehaviour, ILevelController
{
    public static LevelController levelController;
    public Text livesText;
    public Text scoreText;
    public GameObject health;
    public Image meatEnd;
    public Image boneEnd;
    public GameObject levelRetry;
    public GameObject nextLevel;
    public GameObject gameOver;

    void Start()
    {
        levelController = this;
        health.GetComponent<Slider>().maxValue = GameManager.maxHealth;
        health.GetComponent<Slider>().value = GameManager.maxHealth;
        GameManager.StartLevel();
        SetPlayerUIText();
    }

    public void SetPlayerUIText()
    {
        if (GameManager.score < 10) scoreText.text = '0' + GameManager.score.ToString();
        else scoreText.text = GameManager.score.ToString();
        livesText.text = GameManager.lives.ToString();
        health.GetComponent<Slider>().maxValue = GameManager.maxHealth;
        health.GetComponent<RectTransform>().offsetMax = new Vector2((GameManager.maxHealth - 3) * 45f, 0f);
        health.GetComponent<Slider>().value = GameManager.health;
        meatEnd.rectTransform.anchoredPosition = new Vector2(32.5f * GameManager.health, 0f);
        boneEnd.rectTransform.anchoredPosition = new Vector2((GameManager.maxHealth - 3) * 40f, 0f);
    }

    public void GameOver()
    {
        GameManager.Setlives(-1);
        if (GameManager.lives > 0) levelRetry.SetActive(true);
        else gameOver.SetActive(true);
    }

    public void ChangeLevel()
    {
        nextLevel.SetActive(true);
    }
}

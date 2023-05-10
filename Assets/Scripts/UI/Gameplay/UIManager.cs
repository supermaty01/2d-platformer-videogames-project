using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject scoreScreen;
    public TextMeshProUGUI scoreText;

    public GameObject gameOverScreen;
    
    
    int score = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        scoreText.text = score.ToString();
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

    public void ShowGameOverScreen()
    {
        scoreScreen.SetActive(false);
        gameOverScreen.SetActive(true);
    }
}

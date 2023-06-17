using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject scoreScreen;
    public TextMeshProUGUI scoreText;

    public GameObject gameOverScreen;


    private int _score;

    private void Start()
    {
        GameEvents.OnPlayerScoreChangeEvent += AddScore;
        GameEvents.OnGameOverEvent += ShowGameOverScreen;

        scoreText.text = _score.ToString();
    }

    private void OnDestroy()
    {
        GameEvents.OnPlayerScoreChangeEvent -= AddScore;
        GameEvents.OnGameOverEvent -= ShowGameOverScreen;
    }

    private void AddScore(int points)
    {
        _score += points;
        scoreText.text = _score.ToString();
    }

    private void ShowGameOverScreen()
    {
        scoreScreen.SetActive(false);
        gameOverScreen.SetActive(true);
    }
}
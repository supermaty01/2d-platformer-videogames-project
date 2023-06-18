using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject scoreScreen;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverScreen;
    public GameObject finishScreen;


    private int _score;

    private void Start()
    {
        GameEvents.OnPlayerScoreChangeEvent += AddScore;
        GameEvents.OnGameOverEvent += ShowGameOverScreen;
        GameEvents.OnEndLevelEvent += ShowFinishScreen;
        
        scoreText.text = _score.ToString();
    }

    private void OnDestroy()
    {
        GameEvents.OnPlayerScoreChangeEvent -= AddScore;
        GameEvents.OnGameOverEvent -= ShowGameOverScreen;
        GameEvents.OnEndLevelEvent -= ShowFinishScreen;
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

    private void ShowFinishScreen()
    {
        GameManager.Instance.score += _score;
        GameManager.Instance.target.gameObject.SetActive(false);

        scoreScreen.SetActive(false);
        finishScreen.SetActive(true);
    }
}
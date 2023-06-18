using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Button nextLevelButton;

    private void Start()
    {
        scoreText.text = GameManager.Instance.score.ToString();
        nextLevelButton.onClick.AddListener(NextLevel);
    }

    private void NextLevel()
    {
        GameManager.Instance.NextLevel();
    }
}
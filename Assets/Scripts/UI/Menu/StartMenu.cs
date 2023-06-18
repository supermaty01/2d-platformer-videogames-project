using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public Button startButton;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        GameManager.Instance.StarGame();
    }
}
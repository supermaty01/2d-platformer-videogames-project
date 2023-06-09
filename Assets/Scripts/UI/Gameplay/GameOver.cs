using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Button playAgainButton;
    public Button quitButton;

    private void Start()
    {
        playAgainButton.onClick.AddListener(PlayAgain);
        quitButton.onClick.AddListener(Quit);
    }


    public void ShowButtons()
    {
        playAgainButton.transform.gameObject.SetActive(true);
        quitButton.transform.gameObject.SetActive(true);
    }

    private void PlayAgain()
    {
        GameManager.Instance.RetryLevel();
    }

    private void Quit()
    {
        GameManager.Instance.MainMenu();
    }
}
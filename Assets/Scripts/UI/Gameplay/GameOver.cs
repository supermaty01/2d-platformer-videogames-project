using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject playAgainButton;
    public GameObject quitButton;

    public void ShowButtons()
    {
        playAgainButton.SetActive(true);
        quitButton.SetActive(true);
    }

    public void PlayAgain()
    {
        GameManager.Instance.RetryLevel();
    }

    public void Quit()
    {
        GameManager.Instance.MainMenu();
    }
}
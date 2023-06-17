using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        GameManager.Instance.MainMenu();
    }
}

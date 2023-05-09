using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void OnGameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int _score;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        MainMenu();
    }

    private void OnDestroy()
    {
    }

    public static void MainMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void StarGame()
    {
        _score = 0;
        StartCoroutine(LoadGameplayAsyncScene("Level1"));
    }

    private static IEnumerator LoadGameplayAsyncScene(string scene)
    {
        var asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone) yield return null;

        yield return new WaitForSeconds(1f);

        GameEvents.OnStartGameEvent?.Invoke();
    }
    
    public int GetScore()
    {
        return _score;
    }
}
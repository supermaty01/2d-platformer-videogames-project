using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score;
    public Transform target;

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
        AudioManager.Instance.Init();
        MainMenu();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartScreen");
        AudioManager.Instance.PlayMusic(AudioMusicType.Menu);
    }

    public void StarGame()
    {
        score = 0;
        StartCoroutine(LoadGameplayAsyncScene("Level1"));
    }

    public void RetryLevel()
    {
        StartCoroutine(LoadGameplayAsyncScene(SceneManager.GetActiveScene().name));
    }

    private IEnumerator LoadGameplayAsyncScene(string scene)
    {
        var asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone) yield return null;

        yield return new WaitForSeconds(1f);
        
        AudioManager.Instance.PlayMusic(AudioMusicType.Gameplay);
    }
}
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    public static ScenesController Instance;
    public UnityEvent<float> OnProgressUpdate;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void RestartGame()
    {
        var indexScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indexScene);
    }

    public void StartGame()
    {
        StartGameAsync().Forget();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(GlobalConstants.MENU_SCENE_INDEX);
    }

    private async UniTask StartGameAsync()
    {
        var operation = SceneManager.LoadSceneAsync(GlobalConstants.GAME_SCENE_INDEX);
        while (!operation.isDone)
        {
            OnProgressUpdate?.Invoke(operation.progress);
            await UniTask.Yield();
        }
    }
}
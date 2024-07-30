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

    private async UniTask StartGameAsync()
    {
        var startSceneIndex = 1;
        var operation = SceneManager.LoadSceneAsync(startSceneIndex);
        while (!operation.isDone)
        {
            OnProgressUpdate?.Invoke(operation.progress);
            await UniTask.Yield();
        }
    }
}
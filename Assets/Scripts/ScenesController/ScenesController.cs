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

    public void ExitGame()
    {
        Application.Quit();
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    
   private async UniTask StartGameAsync()
   {
       var operation = SceneManager.LoadSceneAsync(GlobalConstants.GAME_SCENE_INDEX);
       operation.allowSceneActivation = false; 
       
       float progress = 0f;
       
       while (operation.progress < 0.9f)
       {
           progress = Mathf.Lerp(progress, operation.progress, Time.deltaTime * 5);
           OnProgressUpdate?.Invoke(progress);
           await UniTask.Yield();
       }
       
       OnProgressUpdate?.Invoke(1.0f);
       operation.allowSceneActivation = true;
   }


}
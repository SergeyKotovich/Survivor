using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    private Button _play;
    private ScenesController _scenesController;

    private void Awake()
    {
        _play = GetComponent<Button>();
        if (_scenesController == null)
        {
            _scenesController = FindFirstObjectByType<ScenesController>();
            _play.onClick.AddListener(_scenesController.StartGame);
        }
    }

    private void OnDestroy()
    {
        if (_scenesController != null && _play != null)
        {
            _play.onClick.RemoveListener(_scenesController.StartGame);
        }
    }
}
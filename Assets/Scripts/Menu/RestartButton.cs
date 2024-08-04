using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    private Button _restartButton;
    private ScenesController _scenesController;

    private void Awake()
    {
        _restartButton = GetComponent<Button>();
        if (_scenesController == null)
        {
            _scenesController = FindFirstObjectByType<ScenesController>();
            _restartButton.onClick.AddListener(_scenesController.RestartGame);
        }
    }

    private void OnDestroy()
    {
        if (_scenesController != null && _restartButton != null)
        {
            _restartButton.onClick.RemoveListener(_scenesController.RestartGame);
        }
    }
}
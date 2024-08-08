using UnityEngine;
using UnityEngine.UI;

public class ReturnToMenuButton : MonoBehaviour
{
    private Button _returnToMenu;
    private ScenesController _scenesController;

    private void Awake()
    {
        _returnToMenu = GetComponent<Button>();
        if (_scenesController == null)
        {
            _scenesController = FindFirstObjectByType<ScenesController>();
            _returnToMenu.onClick.AddListener(_scenesController.ReturnToMenu);
        }
    }

    private void OnDestroy()
    {
        if (_scenesController != null && _returnToMenu != null)
        {
            _returnToMenu.onClick.RemoveListener(_scenesController.ReturnToMenu);
        }
    }
}
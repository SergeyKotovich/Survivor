using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button _play;
    private ScenesController _scenesController;
    private void Awake()
    {
        if (_scenesController == null)
        {
            _scenesController = FindFirstObjectByType<ScenesController>();
            _play.onClick.AddListener(_scenesController.StartGame);
        }
    }
}

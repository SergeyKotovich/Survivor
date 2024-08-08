using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreenController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _youDied;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitGame;
    [SerializeField] private Image _material;
    [SerializeField] private float _timeOffset = 0.5f;
    [SerializeField] private float _endValue = 1f;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private GameObject _rawImage;
    [SerializeField] private GameObject _deathAnimation;
    private ScenesController _scenesController;

    private void Awake()
    {
        if (_scenesController == null)
        {
            _scenesController = FindFirstObjectByType<ScenesController>();
            _restartButton.onClick.AddListener(_scenesController.RestartGame);
            _exitGame.onClick.AddListener(_scenesController.ExitGame);
        }
    }

    public void ShowDeathScreen()
    {
        gameObject.SetActive(true);
        _rawImage.SetActive(true);
        _deathAnimation.SetActive(true);
        var sequence = DOTween.Sequence();
        sequence.Insert(_timeOffset, _material.DOFade(_endValue, _duration));
        sequence.Append(_youDied.DOFade(_endValue, _duration));
        sequence.Join(_youDied.transform.DOScale(Vector3.one, _duration))
            .OnComplete(() =>
            {
                _restartButton.gameObject.SetActive(true);
                _exitGame.gameObject.SetActive(true);
            });
    }
}
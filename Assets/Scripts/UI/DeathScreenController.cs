using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreenController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _youDied;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _exitButton;
    [SerializeField] private Image _material;
    [SerializeField] private float _timeOffset = 0.5f;
    [SerializeField] private float _endValue = 1f;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private GameObject _rawImage;
    [SerializeField] private GameObject _deathAnimation;

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
                _exitButton.gameObject.SetActive(true);
            });

    }
}
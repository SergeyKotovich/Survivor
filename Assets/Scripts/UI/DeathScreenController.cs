using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreenController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _youDied;
    [SerializeField] private Image _material;
    [SerializeField] private float _timeOffset = 0.5f;
    [SerializeField] private float _endValue = 1f;
    [SerializeField] private float _duration = 1f;

    public void ShowDeathScreen()
    {
        gameObject.SetActive(true);
        var sequence = DOTween.Sequence();
        sequence.Insert(_timeOffset, _material.DOFade(_endValue, _duration));
        sequence.Append(_youDied.DOFade(_endValue, _duration));
        sequence.Join(_youDied.transform.DOScale(Vector3.one, _duration));
    }
}
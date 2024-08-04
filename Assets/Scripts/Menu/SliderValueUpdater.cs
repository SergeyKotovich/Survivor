using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SliderValueUpdater : MonoBehaviour
{
    public UnityEvent<float> OnTextUpdated;
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private Slider _slider;

    private void Awake()
    {
        UpdateText(_slider.value);
        _slider.onValueChanged.AddListener(UpdateText);
    }

    private void UpdateText(float value)
    {
        _label.text = value.ToString("0.0");
        OnTextUpdated.Invoke(value);
    }

    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(UpdateText);
    }
}
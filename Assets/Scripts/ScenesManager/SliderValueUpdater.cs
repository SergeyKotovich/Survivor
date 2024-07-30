using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueUpdater : MonoBehaviour
{
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
    }
}
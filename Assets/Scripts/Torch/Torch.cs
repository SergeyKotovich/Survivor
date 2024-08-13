using System;
using UnityEngine;

public class Torch : MonoBehaviour, IImprovementBrightnessTorch
{
    public event Action<float, float> BrightnessTorchUpdated;
    [SerializeField] private Light _torch;
    private PlayerConfig _playerConfig;
    private float _brightness;

    private void Start()
    {
        _brightness = _torch.range;
        BrightnessTorchUpdated?.Invoke(_brightness, _brightness + _playerConfig.BrightnessTorchImprovementStep);
    }
    public void Initialize(PlayerConfig playerConfig)
    {
        _playerConfig = playerConfig;
        _torch.range = playerConfig.BrightnessTorch;
    }
    public void ImproveBrightnessTorch()
    {
        _brightness += _playerConfig.BrightnessTorchImprovementStep;
        _torch.range = _brightness;
        BrightnessTorchUpdated?.Invoke(_brightness, _brightness + _playerConfig.BrightnessTorchImprovementStep);
    }
}
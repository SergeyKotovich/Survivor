using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class LanternLightController : MonoBehaviour
{
    [SerializeField] private Light _light;
    [SerializeField] private float _startIntensity;
    [SerializeField] private float _stepSize;
    private float _maxIntensity = 5f;
    private float _minIntensity = 0;
    
    private bool _isIncreasing = true;

    private void Awake()
    {
        _light.intensity = _startIntensity;
    }

    private void Update()
    {
        if (_light.intensity >= _maxIntensity)
        {
            _isIncreasing = false;
        }

        if (_light.intensity<=_minIntensity)
        {
            _isIncreasing = true;
        }

        if (_isIncreasing)
        {
            _light.intensity += _stepSize;
        }
        else
        {
            _light.intensity -= _stepSize;
        }
    }
    
}
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private Light _light;
    [SerializeField] private int _minLight = 5;
    [SerializeField] private int _maxLight = 40;
    [SerializeField] private int _minValueForAddFirewood = 39;
    [SerializeField] private float _value = 0.0001f;

    protected virtual void Update()
    {
        if (_light.range > _minLight)
        {
            _light.range -= _value;
        }
    }

    public void TryAddFuel(float fuel)
    {
        if (_light.range < _minValueForAddFirewood)
        {
            _light.range += fuel;
            _light.range = Mathf.Clamp(_light.range, _minLight, _maxLight);
        }
    }
}
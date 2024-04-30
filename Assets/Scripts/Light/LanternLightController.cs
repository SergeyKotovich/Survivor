using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class LanternLightController : MonoBehaviour
{
    [SerializeField] private Light _light;

    private void Update()
    {
        var randomNumber = Random.Range(0, 5);
        _light.intensity = randomNumber;
    }
}

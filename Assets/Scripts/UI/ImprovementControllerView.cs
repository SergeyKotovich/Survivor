using System;
using TMPro;
using UnityEngine;

public class ImprovementControllerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentValue;
    [SerializeField] private TextMeshProUGUI _nextValue;
    private IImprover _shootingController;

    public void Initialize(IImprover shootingController)
    {
        _shootingController = shootingController;
        _shootingController.AttackSpeedUpdated += UpdateAttackSpeed;
    }

    private void UpdateAttackSpeed(float currentValue, float nextValue)
    {
        _currentValue.text = currentValue.ToString("0.0");
        _nextValue.text = nextValue.ToString("0.0");
    }

    private void OnDestroy()
    {
        _shootingController.AttackSpeedUpdated -= UpdateAttackSpeed;
    }
}
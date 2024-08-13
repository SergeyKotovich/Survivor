using TMPro;
using UnityEngine;

public class ImprovementControllerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentAttackSpeed;
    [SerializeField] private TextMeshProUGUI _nextAttackSpeed;
    [SerializeField] private TextMeshProUGUI _currentAttackRange;
    [SerializeField] private TextMeshProUGUI _nextAttackRange;
    [SerializeField] private TextMeshProUGUI _currentRunningSpeed;
    [SerializeField] private TextMeshProUGUI _nextRunningSpeed;
    [SerializeField] private TextMeshProUGUI _currentBrightness;
    [SerializeField] private TextMeshProUGUI _nextBrightness;

    private IAtackImprovable _shootingController;
    private ISpeedImprovable _movementController;
    private IImprovementBrightnessTorch _torch;

    public void Initialize(IAtackImprovable shootingController, 
        ISpeedImprovable movementController,
        IImprovementBrightnessTorch torch)
    {
        _torch = torch;
        _movementController = movementController;
        _shootingController = shootingController;
        _shootingController.AttackSpeedUpdated += UpdateAttackSpeed;
        _shootingController.AttackRangeUpdated += UpdateAttackRange;
        _movementController.RunningSpeedUpdated += UpdateRunningSpeed;
        _torch.BrightnessTorchUpdated += UpdateBrightness;
    }

    private void UpdateTextValues(TextMeshProUGUI currentValueText, TextMeshProUGUI nextValueText, float currentValue, float nextValue)
    {
        currentValueText.text = currentValue.ToString("0.0");
        nextValueText.text = nextValue.ToString("0.0");
    }
    
    private void UpdateAttackSpeed(float currentValue, float nextValue)
    {
        UpdateTextValues(_currentAttackSpeed,_nextAttackSpeed, currentValue,nextValue);
    }

    private void UpdateAttackRange(float currentValue, float nextValue)
    {
        UpdateTextValues(_currentAttackRange,_nextAttackRange, currentValue,nextValue);
    }
    private void UpdateRunningSpeed(float currentValue, float nextValue)
    {
        UpdateTextValues(_currentRunningSpeed,_nextRunningSpeed, currentValue,nextValue);
    }
    private void UpdateBrightness(float currentValue, float nextValue)
    {
        UpdateTextValues(_currentBrightness,_nextBrightness, currentValue,nextValue);
    }

    private void OnDestroy()
    {
        _shootingController.AttackSpeedUpdated -= UpdateAttackSpeed;
        _shootingController.AttackRangeUpdated -= UpdateAttackRange;
        _movementController.RunningSpeedUpdated -= UpdateRunningSpeed;
        _torch.BrightnessTorchUpdated -= UpdateBrightness;
    }
}
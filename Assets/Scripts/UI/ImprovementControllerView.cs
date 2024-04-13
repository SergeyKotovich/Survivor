using TMPro;
using UnityEngine;

public class ImprovementControllerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentAttackSpeedValue;
    [SerializeField] private TextMeshProUGUI _nextAttackSpeedValue;
    [SerializeField] private TextMeshProUGUI _currentAttackRangeValue;
    [SerializeField] private TextMeshProUGUI _nextAttackRangeValue;
    private IImprover _shootingController;

    public void Initialize(IImprover shootingController)
    {
        _shootingController = shootingController;
        _shootingController.AttackSpeedUpdated += UpdateAttackSpeed;
     //   _shootingController.AttackRangeUpdated += UpdateAttackRange;
    }

    private void UpdateAttackSpeed(float currentValue, float nextValue)
    {
        _currentAttackSpeedValue.text = currentValue.ToString("0.0");
        _nextAttackSpeedValue.text = nextValue.ToString("0.0");
    }

 // private void UpdateAttackRange(float currentValue, float nextValue)
 // {
 //     _currentAttackRangeValue.text = currentValue.ToString("0.0");
 //     _nextAttackRangeValue.text = nextValue.ToString("0.0");
 // }

    private void OnDestroy()
    {
        _shootingController.AttackSpeedUpdated -= UpdateAttackSpeed;
      //  _shootingController.AttackRangeUpdated -= UpdateAttackRange;
    }
}
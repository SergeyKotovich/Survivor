using TMPro;
using UnityEngine;
using VContainer;

public class PriceControllerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _priceAttackSpeed;
    [SerializeField] private TextMeshProUGUI _priceAttackRange;
    [SerializeField] private TextMeshProUGUI _priceRunningSpeed;
    [SerializeField] private TextMeshProUGUI _priceBrightness;
    [SerializeField] private TextMeshProUGUI _priceHeal;
    private IPrice _shopController;

    [Inject]
    public void Construct(IPrice shopController)
    {
        _shopController = shopController;
        _shopController.AttackSpeedImproved += UpdatePriceAttackSpeed;
        _shopController.AttackRangeImproved += UpdatePriceAttackRange;
        _shopController.RunningSpeedImproved += UpdatePriceRunningSpeed;
        _shopController.DamageImproved += UpdatePriceDamage;
        _shopController.HealImproved += UpdatePriceHeal;
    }
    
    private void UpdatePriceAttackSpeed(int price)
    {
        _priceAttackSpeed.text = price.ToString();
    }
    private void UpdatePriceAttackRange(int price)
    {
        _priceAttackRange.text = price.ToString();
    }
    private void UpdatePriceRunningSpeed(int price)
    {
        _priceRunningSpeed.text = price.ToString();
    }
    private void UpdatePriceDamage(int price)
    {
        _priceBrightness.text = price.ToString();
    }
    private void UpdatePriceHeal(int price)
    {
        _priceHeal.text = price.ToString();
    }

    private void OnDestroy()
    {
        _shopController.AttackSpeedImproved -= UpdatePriceAttackSpeed;
        _shopController.AttackRangeImproved -= UpdatePriceAttackRange;
        _shopController.RunningSpeedImproved -= UpdatePriceRunningSpeed;
        _shopController.DamageImproved -= UpdatePriceDamage;
        _shopController.HealImproved -= UpdatePriceHeal;
    }
}
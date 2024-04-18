using System;
using TMPro;
using UnityEngine;

public class PriceControllerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _priceAttackSpeed;
    [SerializeField] private TextMeshProUGUI _priceAttackRange;
    [SerializeField] private TextMeshProUGUI _priceRunningSpeed;
    [SerializeField] private TextMeshProUGUI _priceDamage;
    [SerializeField] private TextMeshProUGUI _priceHeal;

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
        _priceDamage.text = price.ToString();
    }
    private void UpdatePriceHeal(int price)
    {
        _priceHeal.text = price.ToString();
    }
}
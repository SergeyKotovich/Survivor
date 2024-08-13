using System;
using JetBrains.Annotations;
using MessagePipe;
using UnityEngine;
using VContainer;

public class ShopController : MonoBehaviour, IPrice
{
    public event Action<int> AttackSpeedImproved;
    public event Action<int> AttackRangeImproved;
    public event Action<int> RunningSpeedImproved;
    public event Action<int> DamageImproved;
    public event Action<int> HealImproved;
    
    private Action _attackSpeedImprovementCallback;
    private Action _attackRangeImprovementCallBack;
    private Action _runningSpeedImprovementCallBack;
    private Action _damageImprovementCallBack;
    private Action _healCallBack;
    
    [SerializeField] private ShopConfig _shopConfig;
    
    private Wallet _wallet;
    
    private int _coefficientAttackSpeed = 1;
    private int _coefficientAttackRange = 1;
    private int _coefficientRunningSpeed = 1;
    private int _coefficientDamage = 1;
    
    [Inject]
    public void Construct(Wallet wallet, IImprovable playerController)
    {
        _attackSpeedImprovementCallback = playerController.ImproveAttackSpeed;
        _attackRangeImprovementCallBack = playerController.ImproveAttackRange;
        _runningSpeedImprovementCallBack = playerController.ImproveRunningSpeed;
        _damageImprovementCallBack = playerController.ImproveBrightnessTorch;
        _healCallBack = playerController.Heal;
        _wallet = wallet;
    }

    private void Start()
    {
        AttackSpeedImproved?.Invoke(_shopConfig.PriceImprovement * _coefficientAttackSpeed);
        AttackRangeImproved?.Invoke(_shopConfig.PriceImprovement*_coefficientAttackRange);
        RunningSpeedImproved?.Invoke(_shopConfig.PriceImprovement*_coefficientRunningSpeed);
        DamageImproved?.Invoke(_shopConfig.PriceImprovement*_coefficientDamage);
        HealImproved?.Invoke(_shopConfig.PriceHeal);
    }

    [UsedImplicitly]
    public void SellAttackSpeedImprovement()
    {
        var price = _shopConfig.PriceImprovement * _coefficientAttackSpeed; 
        if (_wallet.TryBuy(price))
        {
            _attackSpeedImprovementCallback?.Invoke();
            _coefficientAttackSpeed++; 
            AttackSpeedImproved?.Invoke(_shopConfig.PriceImprovement * _coefficientAttackSpeed);
        }
    }
    [UsedImplicitly]
    public void SellAttackRangeImprovement()
    {
        var price = _shopConfig.PriceImprovement * _coefficientAttackRange;
        if (_wallet.TryBuy(price))
        {
            _attackRangeImprovementCallBack?.Invoke();
            _coefficientAttackRange++;
            AttackRangeImproved?.Invoke(_shopConfig.PriceImprovement*_coefficientAttackRange);
        }
    }
    [UsedImplicitly]
    public void SellRunningSpeedImprovement()
    {
        var price = _shopConfig.PriceImprovement * _coefficientRunningSpeed;
        if (_wallet.TryBuy(price))
        {
            _runningSpeedImprovementCallBack?.Invoke();
            _coefficientRunningSpeed++;
            RunningSpeedImproved?.Invoke(_shopConfig.PriceImprovement*_coefficientRunningSpeed);
        }
    }
    [UsedImplicitly]
    public void SellDamageImprovement()
    {
        var price = _shopConfig.PriceImprovement *_coefficientDamage;
        if (_wallet.TryBuy(price))
        {
            _damageImprovementCallBack?.Invoke();
            _coefficientDamage++;
            DamageImproved?.Invoke(_shopConfig.PriceImprovement*_coefficientDamage);
        }
    }
    [UsedImplicitly]
    public void SellMedicine()
    {
        if (_wallet.TryBuy(_shopConfig.PriceHeal))
        {
            _healCallBack?.Invoke();
        }
    }
}
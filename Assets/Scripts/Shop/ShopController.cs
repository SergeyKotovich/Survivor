using System;
using JetBrains.Annotations;
using MessagePipe;
using UnityEngine;
using VContainer;

public class ShopController : MonoBehaviour
{
    [SerializeField] private ShopConfig _shopConfig;
    private Wallet _wallet;
    private Action _attackSpeedImprovementCallback;
    private Action _attackRangeImprovementCallBack;
    private Action _runningSpeedImprovementCallBack;
    private Action _damageImprovementCallBack;
    private Action _healCallBack;
    private int _coefficientAttackSpeed;
    private int _coefficientAttackRange;
    private int _coefficientRunningSpeed;
    private int _coefficientDamage;
    

    [Inject]
    public void Construct(Wallet wallet, IImprovable playerController)
    {
        _attackSpeedImprovementCallback = playerController.ImproveAttackSpeed;
        _attackRangeImprovementCallBack = playerController.ImproveAttackRange;
        _runningSpeedImprovementCallBack = playerController.ImproveRunningSpeed;
        _damageImprovementCallBack = playerController.ImproveDamage;
        _healCallBack = playerController.Heal;
        _wallet = wallet;
    }

    [UsedImplicitly]
    public void SellAttackSpeedImprovement()
    {
        _coefficientAttackSpeed++;
        if (_wallet.TryBuy(_shopConfig.PriceImprovement*_coefficientAttackSpeed))
        {
            _attackSpeedImprovementCallback?.Invoke();
        }
    }
    [UsedImplicitly]
    public void SellAttackRangeImprovement()
    {
        _coefficientAttackRange++;
        if (_wallet.TryBuy(_shopConfig.PriceImprovement*_coefficientAttackRange))
        {
            _attackRangeImprovementCallBack?.Invoke();
        }
    }
    [UsedImplicitly]
    public void SellRunningSpeedImprovement()
    {
        _coefficientRunningSpeed++;
        if (_wallet.TryBuy(_shopConfig.PriceImprovement*_coefficientRunningSpeed))
        {
            _runningSpeedImprovementCallBack?.Invoke();
        }
    }
    [UsedImplicitly]
    public void SellDamageImprovement()
    {
        _coefficientDamage++;
        if (_wallet.TryBuy(_shopConfig.PriceImprovement*_coefficientDamage))
        {
            _damageImprovementCallBack?.Invoke();
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
using System;
using JetBrains.Annotations;
using MessagePipe;
using UnityEngine;
using VContainer;

public class ShopController : MonoBehaviour
{
    [SerializeField] private float _currentPrice = 1;
    private Wallet _wallet;
    private IPublisher<UpgradePurchasedMessage> _upgradePurchasedPublisher;
    private Action _attackSpeedImprovementCallback;
    private Action _attackRangeImprovementCallBack;
    private Action _runningSpeedImprovementCallBack;
    private Action _damageImprovementCallBack;
    private Action _healCallBack;

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
        if (_wallet.TryBuy(_currentPrice))
        {
            _attackSpeedImprovementCallback?.Invoke();
        }
    }
    [UsedImplicitly]
    public void SellAttackRangeImprovement()
    {
        if (_wallet.TryBuy(_currentPrice))
        {
            _attackRangeImprovementCallBack?.Invoke();
        }
    }
    [UsedImplicitly]
    public void SellRunningSpeedImprovement()
    {
        if (_wallet.TryBuy(_currentPrice))
        {
            _runningSpeedImprovementCallBack?.Invoke();
        }
    }
    [UsedImplicitly]
    public void SellDamageImprovement()
    {
        if (_wallet.TryBuy(_currentPrice))
        {
            _damageImprovementCallBack?.Invoke();
        }
    }
    [UsedImplicitly]
    public void SellMedicine()
    {
        if (_wallet.TryBuy(_currentPrice))
        {
            _healCallBack?.Invoke();
        }
    }
}
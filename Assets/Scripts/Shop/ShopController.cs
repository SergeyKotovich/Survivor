using System;
using JetBrains.Annotations;
using MessagePipe;
using UnityEngine;
using VContainer;

public class ShopController : MonoBehaviour
{
    [SerializeField] private float _currentPrice = 30;
    private Wallet _wallet;
    private IPublisher<UpgradePurchasedMessage> _upgradePurchasedPublisher;
    private Action _attackSpeedImprovementCallback;
    private Action _attackRangeImprovementCallBack;

    [Inject]
    public void Construct(Wallet wallet, PlayerController playerController)
    {
       _attackSpeedImprovementCallback = playerController.ImproveAttackSpeed;
       _attackRangeImprovementCallBack = playerController.ImproveAttackRange;
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
}
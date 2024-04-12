using System;
using MessagePipe;
using UnityEngine;
using VContainer;

public class UIController : MonoBehaviour
{
    [SerializeField] private EnemyDeathCounter _enemyDeathCounter;
    [SerializeField] private MoneyConverter _moneyConverter;
    [SerializeField] private WalletView _walletView;
    [SerializeField] private BreakControllerView _breakControllerView;
    [SerializeField] private ImprovementControllerView _improvementControllerView;
    private Wallet _wallet;

    [Inject]
    private void Construct(Wallet wallet, PlayerController playerController,
        IPublisher<CountMoneyChangedMessage> countMoneyChangedSPublisher,
        ISubscriber<BreakHasStartedMessage> breakHasStartedSubscriber,
        ISubscriber<EnemyDiedMessage> enemyDiedSubscriber)
    {
        _wallet = wallet;
        _walletView.Initialize(_wallet);
        _enemyDeathCounter.Initialize(enemyDiedSubscriber, breakHasStartedSubscriber);
        _moneyConverter.Initialize(_enemyDeathCounter,countMoneyChangedSPublisher,breakHasStartedSubscriber);
        _breakControllerView.Initialize(breakHasStartedSubscriber);
        _improvementControllerView.Initialize(playerController.ShootingController);
    }
}
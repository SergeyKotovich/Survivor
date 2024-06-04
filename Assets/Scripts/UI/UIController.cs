using System;
using MessagePipe;
using UnityEngine;
using VContainer;

public class UIController : MonoBehaviour
{
    [SerializeField] private EnemyDeathCounter _enemyDeathCounter;
    [SerializeField] private WalletView _walletView;
    [SerializeField] private BreakControllerView _breakControllerView;
    [SerializeField] private ImprovementControllerView _improvementControllerView;
    [SerializeField] private DeathScreenController _deathScreenController;
    [SerializeField] private KateNpcController _kateNpcView;
    private Wallet _wallet;

    [Inject]
    private void Construct(Wallet wallet, PlayerController playerController,
        ISubscriber<EnemyDiedMessage> enemyDiedSubscriber,
        ISubscriber<AllEnemiesDiedMessage> allEnemyDiedSubscriber,
        IPublisher<BreakFinishedMessage> breakFinishedPublisher)
    {
        _wallet = wallet;
        _walletView.Initialize(_wallet);
        _enemyDeathCounter.Initialize(enemyDiedSubscriber);
        _breakControllerView.Initialize(allEnemyDiedSubscriber,breakFinishedPublisher, _kateNpcView.ShowMessages);
        _improvementControllerView.Initialize(playerController.ShootingController, 
            playerController.MovementController,
            playerController.Weapon);
    }

    public void ShowDeathScreen()
    {
        _deathScreenController.ShowDeathScreen();
    }
}
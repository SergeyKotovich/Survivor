using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private bl_Joystick _blJoystick;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private EnemiesSpawner _enemiesSpawner;
    [SerializeField] private EnemiesController _enemiesController;
    [SerializeField] private UIController _uiController;
    [SerializeField] private WavesController _wavesController;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterInput(builder);
        
        builder.Register<EnemyFactory>(Lifetime.Singleton);
        builder.Register<Wallet>(Lifetime.Singleton);

        builder.RegisterInstance(_enemiesController).AsImplementedInterfaces();
        builder.RegisterInstance(_enemiesSpawner);
        builder.RegisterInstance(_playerController).AsImplementedInterfaces();
        builder.RegisterInstance(_uiController);
        builder.RegisterInstance(_wavesController);

        builder.RegisterEntryPoint<GameController>();
        
        RegisterMessagePipe(builder);
    }

    private void RegisterMessagePipe(IContainerBuilder builder)
    {
        var options = builder.RegisterMessagePipe();
        builder.RegisterBuildCallback(c=>GlobalMessagePipe.SetProvider(c.AsServiceProvider()));
        
        builder.RegisterMessageBroker<EnemySpawnedMessage>(options);
        builder.RegisterMessageBroker<EnemyDiedMessage>(options);
        builder.RegisterMessageBroker<EnemyIsNearbyMessage>(options);
        builder.RegisterMessageBroker<AllEnemiesSpawnedMessage>(options);
        builder.RegisterMessageBroker<AllEnemiesDiedMessage>(options);

        builder.RegisterMessageBroker<BreakFinishedMessage>(options);
        builder.RegisterMessageBroker<CountMoneyChangedMessage>(options);
        
        builder.RegisterMessageBroker<UpgradePurchasedMessage>(options);
        builder.RegisterMessageBroker<PlayerDiedMessage>(options);
    }

    private void RegisterInput(IContainerBuilder builder)
    {
        if (Application.isMobilePlatform)
        {
            _blJoystick.gameObject.SetActive(true);

            builder.RegisterInstance(_blJoystick);

            builder.Register<MobilInputHandler>(Lifetime.Singleton).AsImplementedInterfaces();
        }
        else
        {
            builder.Register<StandaloneInputHandler>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
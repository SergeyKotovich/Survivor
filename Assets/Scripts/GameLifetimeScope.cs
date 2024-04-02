using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private bl_Joystick _blJoystick;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private EnemiesController _enemiesController;
    [SerializeField] private Weapon _weapon;
    
    protected override void Configure(IContainerBuilder builder)
    {
        RegisterInput(builder);
        
        builder.Register<EnemyFactory>(Lifetime.Singleton);
        
        builder.RegisterInstance(_weapon).AsImplementedInterfaces();
        builder.RegisterInstance(_enemiesController).AsImplementedInterfaces();
        builder.RegisterInstance(_enemySpawner);
        builder.RegisterInstance(_playerController).AsImplementedInterfaces();
        
        builder.RegisterEntryPoint<GameController>();
        
        RegisterMessagePipe(builder);
    }

    private void RegisterMessagePipe(IContainerBuilder builder)
    {
        var options = builder.RegisterMessagePipe();
        builder.RegisterBuildCallback(c=>GlobalMessagePipe.SetProvider(c.AsServiceProvider()));
        builder.RegisterMessageBroker<EnemySpawnedMessage>(options);
        builder.RegisterMessageBroker<EnemyDiedMessage>(options);
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
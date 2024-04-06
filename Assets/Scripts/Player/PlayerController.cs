using System;
using MessagePipe;
using UnityEngine;
using VContainer;

public class PlayerController : MonoBehaviour, IMovable
{
    public Vector3 Position => transform.position;
    public IHealth HealthController => _healthController;

    [SerializeField] private PlayerMovementController _playerMovementController;
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private PlayerAnimationController _playerAnimationController;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private ShootingController _shootingController;
    [SerializeField] private PlayerTargetController _playerTargetController;

    private HealthController _healthController;
    private IPublisher<PlayerDiedMessage> _playerDiedPublisher;

    [Inject]
    public void Construct(IPublisher<PlayerDiedMessage> playerDiedPublisher)
    {
        _playerDiedPublisher = playerDiedPublisher;
        _healthController = new HealthController(_playerConfig.Health);
        _healthController.Died += OnPlayerDied;
        _shootingController.CanShoot += Shoot;
        Initialize();
    }

    private void Initialize()
    {
        _playerMovementController.Initialize(_playerConfig.Speed);
        _shootingController.Initialize(_playerTargetController);
        _weapon.Initialize(_playerConfig.Damage);
    }

    private void Shoot()
    { 
        _weapon.Shoot();
    }
    
    public void TakeDamage(float damage)
    {
        var healthController = (IHealthHandler)_healthController;
        healthController.TakeDamage(damage);
    }

    private void OnPlayerDied()
    {
        _playerDiedPublisher.Publish(new PlayerDiedMessage());
        _playerAnimationController.PlayDeathAnimation();
    }

    private void OnDestroy()
    {
        _healthController.Died -= OnPlayerDied;
        _shootingController.CanShoot -= Shoot;
    }
}
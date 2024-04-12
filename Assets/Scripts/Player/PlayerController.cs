using System;
using MessagePipe;
using UnityEngine;
using VContainer;

public class PlayerController : MonoBehaviour, IMovable , IHealthHandler
{
    public Vector3 Position => transform.position;
    public IHealth HealthController => _healthController;
    public IImprover ShootingController => _shootingController;

    [SerializeField] private PlayerMovementController _playerMovementController;
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private PlayerAnimationController _playerAnimationController;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private ShootingController _shootingController;
    [SerializeField] private PlayerTargetController _playerTargetController;

    private HealthController _healthController;
    private IPublisher<PlayerDiedMessage> _playerDiedPublisher;

    [Inject]
    public void Construct(IPublisher<PlayerDiedMessage> playerDiedPublisher,
        ISubscriber<UpgradePurchasedMessage> upgradePurchasedSubscriber)
    {
        _playerDiedPublisher = playerDiedPublisher;
        _healthController = new HealthController(_playerConfig.Health);
        _healthController.Died += OnPlayerDied;
        _shootingController.CanShoot += Shoot;
        Initialize(upgradePurchasedSubscriber);
    }

    private void Initialize(ISubscriber<UpgradePurchasedMessage> upgradePurchasedSubscriber)
    {
        _playerMovementController.Initialize(_playerConfig.RunningSpeed);
        _shootingController.Initialize(_playerTargetController, _playerConfig, upgradePurchasedSubscriber);
        _weapon.Initialize(_playerConfig.Damage);
    }
    
    public void TakeDamage(float damage)
    {
        var healthController = (IHealthHandler)_healthController;
        healthController.TakeDamage(damage);
    }
    public void ImproveAttackSpeed()
    {
        _shootingController.ImproveAttackSpeed();
    }

    public void ImproveAttackRange()
    {
        _shootingController.ImproveAttackRange();
    }

    private void Shoot()
    { 
        _weapon.Shoot();
    }
    
    private void OnPlayerDied()
    {
        _playerDiedPublisher.Publish(new PlayerDiedMessage());
        _playerAnimationController.PlayDeathAnimation();
        _shootingController.CanShoot -= Shoot;
    }

    private void OnDestroy()
    {
        _healthController.Died -= OnPlayerDied;
    }
}
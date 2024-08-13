using System;
using MessagePipe;
using UnityEngine;
using VContainer;

public class PlayerController : MonoBehaviour, IMovable, IImprovable
{
    public Vector3 Position => transform.position;
    public IHealth HealthController => _healthController;
    public IAtackImprovable ShootingController => _shootingController;
    public ISpeedImprovable MovementController => _playerMovementController;
    public IImprovementBrightnessTorch Torch => _torch;

    [SerializeField] private PlayerMovementController _playerMovementController;
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private PlayerAnimationController _playerAnimationController;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private ShootingController _shootingController;
    [SerializeField] private PlayerTargetController _playerTargetController;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private TriggerHandler _triggerHandler;
    [SerializeField] private Torch _torch;

    private HealthController _healthController;
    private IPublisher<PlayerDiedMessage> _playerDiedPublisher;

    [Inject]
    public void Construct(IPublisher<PlayerDiedMessage> playerDiedPublisher,
        IPublisher<MoneyCollectedMessage> moneyCollectedPublisher)
    {
        _playerDiedPublisher = playerDiedPublisher;
        _healthController = new HealthController(_playerConfig.Health);
        _healthController.Died += OnPlayerDied;
        _shootingController.CanShoot += Shoot;
        Initialize(moneyCollectedPublisher);
    }

    private void Initialize(IPublisher<MoneyCollectedMessage> moneyCollectedPublisher)
    {
        _playerMovementController.Initialize(_playerConfig);
        _shootingController.Initialize(_playerTargetController, _playerConfig);
        _weapon.Initialize(_playerConfig);
        _triggerHandler.Initialize(_inventory, moneyCollectedPublisher);
        _torch.Initialize(_playerConfig);
    }

    public void TakeDamage(float damage)
    {
        _healthController.TakeDamage(damage);
    }

    public void Heal()
    {
        _healthController.Heal();
        SoundsManager.Instance.PlayHealSound();
    }

    public void ImproveAttackSpeed()
    {
        _shootingController.ImproveAttackSpeed();
    }

    public void ImproveAttackRange()
    {
        _shootingController.ImproveAttackRange();
    }

    public void ImproveRunningSpeed()
    {
        _playerMovementController.ImproveRunningSpeed();
    }

    public void ImproveBrightnessTorch()
    {
        _torch.ImproveBrightnessTorch();
    }

    private void Shoot()
    {
        _weapon.Shoot();
        SoundsManager.Instance.PlayShot();
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
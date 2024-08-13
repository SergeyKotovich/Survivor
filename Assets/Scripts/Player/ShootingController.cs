using System;
using UnityEngine;

public class ShootingController : MonoBehaviour, IAtackImprovable
{
    public event Action CanShoot;
    public event Action<float, float> AttackSpeedUpdated;
    public event Action<float, float> AttackRangeUpdated;

    private float _attackRange;
    private float _cooldownAfterShot;
    private float _currentTime;

    private ITarget _targetController;
    private PlayerConfig _playerConfig;

    private void Start()
    {
        AttackSpeedUpdated?.Invoke(_cooldownAfterShot, _cooldownAfterShot - _playerConfig.AttackImprovementStep);
        AttackRangeUpdated?.Invoke(_attackRange, _attackRange + _playerConfig.AttackImprovementStep);
    }

    public void Initialize(ITarget targetController, PlayerConfig playerConfig)
    {
        _playerConfig = playerConfig;
        _targetController = targetController;
        _cooldownAfterShot = playerConfig.AttackSpeed;
        _attackRange = playerConfig.AttackRange;
    }

    public void Update()
    {
        if (!_targetController.HasTarget())
        {
            return;
        }

        var nearestEnemy = _targetController.GetTarget();
        _currentTime += Time.deltaTime;
        if (Vector3.Distance(transform.position, nearestEnemy.transform.position) > _attackRange)
        {
            return;
        }

        if (_currentTime >= _cooldownAfterShot)
        {
            CanShoot?.Invoke();
            _currentTime = 0;
        }
    }

    public void ImproveAttackSpeed()
    {
        _cooldownAfterShot -= _playerConfig.AttackImprovementStep;
        AttackSpeedUpdated?.Invoke(_cooldownAfterShot, _cooldownAfterShot - _playerConfig.AttackImprovementStep);
    }

    public void ImproveAttackRange()
    {
        _attackRange += _playerConfig.AttackImprovementStep;
        AttackRangeUpdated?.Invoke(_attackRange, _attackRange + _playerConfig.AttackImprovementStep);
    }
}
using System;
using JetBrains.Annotations;
using MessagePipe;
using UnityEngine;
using VContainer;

public class ShootingController : MonoBehaviour, IAtackImprovable
{
    public event Action CanShoot;
    public event Action<float, float> AttackSpeedUpdated; 
    public event Action<float, float> AttackRangeUpdated; 

    private float _attackRange ;
    private float _delayBetweenShots ;
    private float _currentTime;
    
    private ITarget _targetController;
    private PlayerConfig _playerConfig;

    private void Start()
    {
        AttackSpeedUpdated?.Invoke(_delayBetweenShots, _delayBetweenShots-_playerConfig.AttackImprovementStep);
        AttackRangeUpdated?.Invoke(_attackRange, _attackRange+_playerConfig.AttackImprovementStep);
    }

    public void Initialize(ITarget targetController, PlayerConfig playerConfig)
    {
        _playerConfig = playerConfig;
        _targetController = targetController;
        _delayBetweenShots = playerConfig.AttackSpeed;
        _attackRange = playerConfig.AttackRange;
    }
    
    public void Update()
    {
        _currentTime += Time.deltaTime;
       
        if (!_targetController.HasTarget())
        {
            return;
        }
        var nearestEnemy = _targetController.GetTarget();
        if (Vector3.Distance(transform.position, nearestEnemy.transform.position)>_attackRange)
        {
            return;
        }
        if (_currentTime>=_delayBetweenShots)
        {
            CanShoot?.Invoke();
            _currentTime = 0;
        }
    }

    public void ImproveAttackSpeed()
    {
        _delayBetweenShots -= _playerConfig.AttackImprovementStep;
        AttackSpeedUpdated?.Invoke(_delayBetweenShots, _delayBetweenShots - _playerConfig.AttackImprovementStep); 
    }

    public void ImproveAttackRange()
    {
        _attackRange += _playerConfig.AttackImprovementStep;
        AttackRangeUpdated?.Invoke(_attackRange, _attackRange+_playerConfig.AttackImprovementStep);
    }
}
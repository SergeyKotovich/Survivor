using System;
using UnityEngine;
using VContainer;

public class ShootingController : MonoBehaviour
{
    public event Action CanShoot;
    
    private float _minDistance = 8;
    private float _shootingTime = 1f;
    private float _currentTime;
    
    private ITarget _targetController;
    
    public void Initialize(ITarget targetController)
    {
        _targetController = targetController;
    }
    public void Update()
    {
        _currentTime += Time.deltaTime;
       
        if (!_targetController.HasTarget())
        {
            return;
        }
        var nearestEnemy = _targetController.GetTarget();
        if (Vector3.Distance(transform.position, nearestEnemy.transform.position)>_minDistance)
        {
            return;
        }
        if (_currentTime>=_shootingTime)
        {
            CanShoot?.Invoke();
            _currentTime = 0;
        }
    }
}
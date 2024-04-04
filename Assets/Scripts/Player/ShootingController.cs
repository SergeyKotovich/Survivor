using System;
using UnityEngine;
using VContainer;

public class ShootingController : MonoBehaviour
{
    private float _minDistance = 8;
    private float _shootingTime = 1f;
    private float _currentTime;
    
    private ITarget _target;
    private IWeapon _weapon;

    private void Awake()
    {
        _weapon = GetComponent<IWeapon>();
    }

    [Inject] 
    public void Construct(ITarget target )
    {
        _target = target;
    }
    public void Update()
    {
        _currentTime += Time.deltaTime;
       
        if (!_target.HasTarget())
        {
            return;
        }
        var nearestEnemy = _target.GetTarget();
        if (Vector3.Distance(transform.position, nearestEnemy.transform.position)>_minDistance)
        {
            return;
        }
        if (_currentTime>=_shootingTime)
        {
            _weapon.Shoot();
            _currentTime = 0;
        }


    }

}
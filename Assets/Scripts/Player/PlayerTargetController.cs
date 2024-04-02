using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

public class PlayerTargetController : MonoBehaviour
{
    private int _minDistance = 10;
    private IEnemiesController _enemiesController;
    
    private float _currentTime;
    public float _shootingTime = 2f;

    [Inject] 
    public void Construct(IEnemiesController enemiesController)
    {
        _enemiesController = enemiesController;
    }


    public void Update()
    {
        if (!_enemiesController.HasAliveEnemies())
        {
            return;
        }
        
        var nearestEnemy = FindNearestEnemy();

        transform.LookAt(nearestEnemy.transform.position);
        _currentTime += Time.deltaTime;
         if (_currentTime>=_shootingTime)
         {
             if (Vector3.Distance(transform.position, nearestEnemy.transform.position)<=_minDistance)
             {
                 var direction = (nearestEnemy.transform.position - transform.position);
                // _weapon.Shoot(direction);
                 _currentTime = 0;
             }
         }
    }

    private Enemy FindNearestEnemy()
    {
        var nearestEnemy = _enemiesController.AliveEnemies.First();
       
        var minDistance = Vector3.Distance(transform.position, nearestEnemy.transform.position);
        foreach (var enemy in _enemiesController.AliveEnemies)
        {
            var distanceToCurrentEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToCurrentEnemy < minDistance)
            {
                nearestEnemy = enemy;
                minDistance = distanceToCurrentEnemy;
            }
        }

        return nearestEnemy;
    }
}
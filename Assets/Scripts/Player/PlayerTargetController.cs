using System;
using System.Linq;
using MessagePipe;
using UnityEngine;
using VContainer;

public class PlayerTargetController : MonoBehaviour, ITarget
{
    private Enemy _nearestEnemy;
    private IEnemiesController _enemiesController;
    private float _minDistance = 10f;
    private IPublisher<EnemyIsNearbyMessage> _enemyIsNearbyPublisher;
    private IDisposable _subscriber;

    [Inject] 
    public void Construct(IEnemiesController enemiesController,
        IPublisher<EnemyIsNearbyMessage> enemyIsNearbyPublisher,
        ISubscriber<EnemyDiedMessage> enemyDiedSubscriber)
    {
        _enemyIsNearbyPublisher = enemyIsNearbyPublisher;
        _enemiesController = enemiesController;
        _subscriber = enemyDiedSubscriber.Subscribe(_=>OnNearestEnemyDied());
    }

    private void Update()
    {
        if (!_enemiesController.HasAliveEnemies())
        {
            _enemyIsNearbyPublisher.Publish(new EnemyIsNearbyMessage(false));
            return;
        }
        FindNearestEnemy();
        if (_nearestEnemy == null) 
        {
            return;
        }
        if (Vector3.Distance(transform.position,_nearestEnemy.transform.position)<_minDistance)
        {
            transform.LookAt(_nearestEnemy.transform.position);
            _enemyIsNearbyPublisher.Publish(new EnemyIsNearbyMessage(true));
                
        }
        else
        {
            _enemyIsNearbyPublisher.Publish(new EnemyIsNearbyMessage(false));
        }
    }

    private void FindNearestEnemy()
    {
        _nearestEnemy = _enemiesController.AliveEnemies.First();
       
        var minDistance = Vector3.Distance(transform.position, _nearestEnemy.transform.position);
        foreach (var enemy in _enemiesController.AliveEnemies)
        {
            var distanceToCurrentEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToCurrentEnemy < minDistance)
            {
                _nearestEnemy = enemy;
                minDistance = distanceToCurrentEnemy;
            }
        }
    }

    public Enemy GetTarget()
    {
        return _nearestEnemy;
    }

    public bool HasTarget()
    {
        return _nearestEnemy != null;
    }

    private void OnNearestEnemyDied()
    {
        _nearestEnemy = null;
    }

    private void OnDestroy()
    {
        _subscriber.Dispose();
    }
}
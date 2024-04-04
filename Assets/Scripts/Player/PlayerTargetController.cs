using System;
using System.Linq;
using MessagePipe;
using UnityEngine;
using VContainer;

public class PlayerTargetController : MonoBehaviour
{
    public Enemy NearestEnemy { get; private set; }
    
    private IEnemiesController _enemiesController;
    private float _minDistance = 10f;
    private IPublisher<EnemyIsNearbyMessage> _enemyIsNearbyPublisher;
    private IDisposable _subscriber;

    [Inject] 
    public void Construct(IEnemiesController enemiesController, IPublisher<EnemyIsNearbyMessage> enemyIsNearbyPublisher, ISubscriber<EnemyDiedMessage> enemyDiedSubscriber)
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
        if (NearestEnemy!=null)
        {
            if (Vector3.Distance(transform.position,NearestEnemy.transform.position)<_minDistance)
            {
                _enemyIsNearbyPublisher.Publish(new EnemyIsNearbyMessage(true));
                transform.LookAt(NearestEnemy.transform.position);
            }
            else
            {
                _enemyIsNearbyPublisher.Publish(new EnemyIsNearbyMessage(false));
            }
        }
    }

    private void FindNearestEnemy()
    {
        NearestEnemy = _enemiesController.AliveEnemies.First();
       
        var minDistance = Vector3.Distance(transform.position, NearestEnemy.transform.position);
        foreach (var enemy in _enemiesController.AliveEnemies)
        {
            var distanceToCurrentEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToCurrentEnemy < minDistance)
            {
                NearestEnemy = enemy;
                minDistance = distanceToCurrentEnemy;
            }
        }
    }

    public bool HasTarget()
    {
        return NearestEnemy != null;
    }

    private void OnNearestEnemyDied()
    {
        NearestEnemy = null;
    }
}
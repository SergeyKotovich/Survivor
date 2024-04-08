using System;
using System.Collections.Generic;
using MessagePipe;
using UnityEngine;
using VContainer;

public class EnemiesController : MonoBehaviour, IEnemiesController
{
    public List<Enemy> AliveEnemies { get; } = new();
    
    private IDisposable _subscription;
    private EnemiesSpawner _enemiesSpawner;

    [Inject]
    public void Construct(ISubscriber<EnemySpawnedMessage> enemySpawnedSubscriber, ISubscriber<EnemyDiedMessage> enemyDiedSubscriber)
    {
        _subscription = DisposableBag.Create(enemySpawnedSubscriber.Subscribe(AddEnemy), enemyDiedSubscriber.Subscribe(RemoveEnemy)) ;
    }
    
    private void AddEnemy(EnemySpawnedMessage enemySpawnedMessage)
    {
        AliveEnemies.Add(enemySpawnedMessage.Enemy);
    }

    private void RemoveEnemy(EnemyDiedMessage enemyDiedMessage)
    {
        AliveEnemies.Remove(enemyDiedMessage.Enemy);
    }
    public bool HasAliveEnemies()
    {
        return AliveEnemies.Count != 0;
    }

    private void OnDestroy()
    {
        _subscription.Dispose();
    }
}
using System;
using System.Collections.Generic;
using MessagePipe;
using UnityEngine;
using VContainer;

public class EnemiesController : MonoBehaviour, IEnemiesController
{
    public List<Enemy> AliveEnemies { get; } = new();
    
    private IDisposable _subscription;
    private EnemySpawner _enemySpawner;

    [Inject]
    public void Construct(ISubscriber<EnemySpawnedMessage> enemySpawnedSubscriber)
    {
        _subscription = enemySpawnedSubscriber.Subscribe(AddEnemy);;
    }
    
    private void AddEnemy(EnemySpawnedMessage enemySpawnedMessage)
    {
        AliveEnemies.Add(enemySpawnedMessage.Enemy);
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
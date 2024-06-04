using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

public class EnemyFactory : IDisposable
{
    private readonly IObjectResolver _container;
    private readonly IDisposable _subscriber;
    
    private readonly ObjectPool<Enemy> _enemyPool;
    private EnemyConfig _enemyConfig;
    private Vector3 _position;
    private CoinsFactory _coinsFactory;

    public EnemyFactory(IObjectResolver container,
        ISubscriber<EnemyDiedMessage> enemyDiedSubscriber, 
         CoinsFactory coinsFactory)
    {
        _coinsFactory = coinsFactory;
        _container = container;
        _subscriber =  enemyDiedSubscriber.Subscribe(enemyDiedMessage => OnEnemyDied(enemyDiedMessage));
        _enemyPool = new ObjectPool<Enemy>(Create, Get, Release);
    }

    public Enemy Spawn(EnemyConfig enemyConfig, Vector3 position)
    {
        _position = position;
        _enemyConfig = enemyConfig;
        var enemy = _enemyPool.Get();
        enemy.Initialize(enemyConfig);
        enemy.transform.position = _position;
        return enemy;
    }

    private Enemy Create()
    {
        return _container.Instantiate(_enemyConfig.EnemyPrefab, _position, Quaternion.identity);
    }

    private void Get(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }

    private void Release(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        enemy.transform.position = Vector3.zero;
        enemy.transform.rotation = Quaternion.identity;
    }

    private async UniTask OnEnemyDied(EnemyDiedMessage enemyDiedMessage)
    {
        await UniTask.Delay(_enemyConfig.DelayAfterDeath);
        var lastEnemyPosition = enemyDiedMessage.Enemy.transform.position;
        _enemyPool.Release(enemyDiedMessage.Enemy);
        _coinsFactory.Spawn(lastEnemyPosition);
    }


    public void Dispose()
    {
        _subscriber.Dispose();
    }
}
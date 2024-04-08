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
    private IObjectResolver _objectResolver;
    private IDisposable _subscriber;
    
    private ObjectPool<Enemy> _enemyPool;
    private EnemyConfig _enemyConfig;
    private Vector3 _position;

    public EnemyFactory(IObjectResolver objectResolver, ISubscriber<EnemyDiedMessage> enemyDiedSubscriber)
    {
        _objectResolver = objectResolver;
        _subscriber =  enemyDiedSubscriber.Subscribe(enemyDiedMessage => OnEnemyDied(enemyDiedMessage));
        _enemyPool = new ObjectPool<Enemy>(Create, Get, Release);
    }

    public Enemy GetEnemy(EnemyConfig enemyConfig, Vector3 position)
    {
        _position = position;
        _enemyConfig = enemyConfig;
        var enemy = _enemyPool.Get();
        enemy.Initialize(enemyConfig);
        return enemy;
    }

    private Enemy Create()
    {
        return _objectResolver.Instantiate(_enemyConfig.EnemyPrefab, _position, Quaternion.identity);
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
        _enemyPool.Release(enemyDiedMessage.Enemy);
    }


    public void Dispose()
    {
        _subscriber.Dispose();
    }
}
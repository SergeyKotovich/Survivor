using System;
using Cysharp.Threading.Tasks;
using MessagePipe;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private SpawnParameters[] _spawnParameters;
    [SerializeField] private int _delayBetweenSpawn;
    
    private EnemyFactory _enemyFactory;
    private IPublisher<EnemySpawnedMessage> _enemySpawnedPublisher;
    private IDisposable _subscriber;
    private bool _playerAlive = true;
    private IPublisher<AllEnemiesSpawnedMessage> _allEnemiesSpawnedPublisher;

    [Inject]
    public void Construct (EnemyFactory enemyFactory,
        IPublisher<EnemySpawnedMessage> enemySpawnedPublisher,
        ISubscriber<PlayerDiedMessage> playerDiedSubscriber,
        IPublisher<AllEnemiesSpawnedMessage> allEnemiesSpawnedPublisher)
    {
        _allEnemiesSpawnedPublisher = allEnemiesSpawnedPublisher;
        _enemySpawnedPublisher = enemySpawnedPublisher;
        _enemyFactory = enemyFactory;
        _subscriber = playerDiedSubscriber.Subscribe( _ =>_playerAlive= false);
    }

    public async UniTask SpawnEnemies(int coefficient)
    {
        foreach (var spawnParameters in _spawnParameters)
        {
            for (int i = 0; i < spawnParameters.CountEnemy*coefficient; i++)
            {
                if (!_playerAlive)
                {
                    return;
                }
                var randomVector = Random.insideUnitSphere;
                randomVector.y = 0;
                var position = spawnParameters.Point.position + spawnParameters.Radius * randomVector;
                await UniTask.Delay(_delayBetweenSpawn);
                var enemy = _enemyFactory.GetEnemy(spawnParameters.EnemyConfig, position);
                _enemySpawnedPublisher.Publish(new EnemySpawnedMessage(enemy));
            }
        }
        _allEnemiesSpawnedPublisher.Publish(new AllEnemiesSpawnedMessage());
    }

    private void OnDestroy()
    {
        _subscriber.Dispose();
    }
}
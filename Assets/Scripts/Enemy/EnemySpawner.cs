using System;
using Cysharp.Threading.Tasks;
using MessagePipe;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpawnParameters[] _spawnParameters;
    [SerializeField] private int _delayBetweenSpawn;
    
    private EnemyFactory _enemyFactory;
    private IPublisher<EnemySpawnedMessage> _enemySpawnedPublisher;
    private IDisposable _subscriber;
    private bool _playerAlive = true;

    [Inject]
    public void Construct (EnemyFactory enemyFactory , IPublisher<EnemySpawnedMessage> enemySpawnedPublisher, ISubscriber<PlayerDiedMessage> playerDiedSubscriber)
    {
        _enemySpawnedPublisher = enemySpawnedPublisher;
        _enemyFactory = enemyFactory;
        _subscriber = playerDiedSubscriber.Subscribe( _ =>_playerAlive= false);
    }

    public async UniTask SpawnEnemies()
    {
        foreach (var spawnParameters in _spawnParameters)
        {
            for (int i = 0; i < spawnParameters.CountEnemy; i++)
            {
                if (_playerAlive)
                {
                    var randomVector = Random.insideUnitSphere;
                    randomVector.y = 0;
                    var position = spawnParameters.Point.position + spawnParameters.Radius * randomVector;
                    await UniTask.Delay(_delayBetweenSpawn);
                    var enemy = _enemyFactory.GetEnemy(spawnParameters.EnemyConfig, position);
                    _enemySpawnedPublisher.Publish(new EnemySpawnedMessage(enemy));
                }
                
            }
            
        }
    }

    private void OnDestroy()
    {
        _subscriber.Dispose();
    }
}
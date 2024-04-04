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
    
    private EnemyFactory _enemyFactory;
    private IPublisher<EnemySpawnedMessage> _enemySpawnedPublisher;

    [Inject]
    public void Construct (EnemyFactory enemyFactory , IPublisher<EnemySpawnedMessage> enemySpawnedPublisher)
    {
        _enemySpawnedPublisher = enemySpawnedPublisher;
        _enemyFactory = enemyFactory;
    }

    public async UniTask SpawnEnemies()
    {
        foreach (var spawnParameters in _spawnParameters)
        {
            for (int i = 0; i < spawnParameters.CountEnemy; i++)
            {
                var randomVector = Random.insideUnitSphere;
                randomVector.y = 0;
                var position = spawnParameters.Point.position + spawnParameters.Radius * randomVector;
                await UniTask.Delay(1000);
                var enemy = _enemyFactory.CreateEnemy(spawnParameters.EnemyConfig, position);
                _enemySpawnedPublisher.Publish(new EnemySpawnedMessage(enemy));
            }
            
        }
    }
}
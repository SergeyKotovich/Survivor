using System;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class WavesController : MonoBehaviour
{
    private EnemiesSpawner _enemiesSpawner;
    private int _currentWave;
 //   private bool _allEnemiesSpawned;
    private bool _allEnemiesDied;
    private IDisposable _subscribers;
    private IPublisher<BreakHasStartedMessage> _breakHasStartedPublisher;

    [Inject]
    public void Construct(EnemiesSpawner enemiesSpawner,
        IPublisher<BreakHasStartedMessage> breakHasStartedPublisher,
        ISubscriber<AllEnemiesDiedMessage>allEnemiesDiedSubscriber)
    {
        _breakHasStartedPublisher = breakHasStartedPublisher;
        _enemiesSpawner = enemiesSpawner;
        _subscribers = //DisposableBag.Create(allEnemiesSpawnedSubscriber.Subscribe(_ => _allEnemiesSpawned = true),
            allEnemiesDiedSubscriber.Subscribe(_ =>_allEnemiesDied = true);
    }

    public void Update()
    {
       // if (_allEnemiesSpawned)
       // {
            if (_allEnemiesDied)
            {
                _allEnemiesDied = false;
             //   _allEnemiesSpawned = false;
                _breakHasStartedPublisher.Publish(new BreakHasStartedMessage());
            }
     //   }
    }

    public async void StartSpawn()
    {
        await _enemiesSpawner.SpawnEnemies();
        _currentWave++;
    }
   
    public void OnDestroy()
    {
        _subscribers.Dispose();
    }
}
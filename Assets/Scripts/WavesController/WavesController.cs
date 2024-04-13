using System;
using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class WavesController : MonoBehaviour
{
    private EnemiesSpawner _enemiesSpawner;
    private int _coefficient;

    private bool _allEnemiesDied;
    private IDisposable _subscribers;

    [Inject]
    public void Construct(EnemiesSpawner enemiesSpawner, ISubscriber<BreakFinishedMessage> breakFinishedSubscriber)
    {
        _enemiesSpawner = enemiesSpawner;
        _subscribers = breakFinishedSubscriber.Subscribe(_ => StartSpawn());
    }
    
    public async void StartSpawn()
    {
        _coefficient++;
        await _enemiesSpawner.SpawnEnemies(_coefficient);
    }
   
    public void OnDestroy()
    {
        _subscribers.Dispose();
    }
}
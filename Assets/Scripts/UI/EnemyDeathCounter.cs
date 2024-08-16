using System;
using MessagePipe;
using TMPro;
using UnityEngine;

public class EnemyDeathCounter : MonoBehaviour, ICounter
{
    public float Count { get; private set; }

    [SerializeField] private TextMeshProUGUI _counter;
    private IDisposable _subscriber;

    public void Initialize(ISubscriber<EnemyDiedMessage> enemyDiedSubscriber,
        ISubscriber<PlayerDiedMessage> playerDiedSubscriber)
    {
        _subscriber = DisposableBag.Create(enemyDiedSubscriber.Subscribe(_ => UpdateCountDiedEnemies()),
            playerDiedSubscriber.Subscribe(_ =>
                SaveCountKilledEnemies()));
    }

    private void SaveCountKilledEnemies()
    {
        Leaderboard.Instance.SaveCurrentScore((int)Count);
    }

    private void UpdateCountDiedEnemies()
    {
        Count++;
        _counter.text = Count.ToString("0");
    }

    private void OnDestroy()
    {
        _subscriber.Dispose();
    }
}
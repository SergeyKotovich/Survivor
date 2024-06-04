using System;
using System.Collections;
using MessagePipe;
using TMPro;
using UnityEngine;
using VContainer;

public class EnemyDeathCounter : MonoBehaviour, ICounter
{
   public float Count { get; private set; }
   
   [SerializeField] private TextMeshProUGUI _counter;
   
   private IDisposable _subscriber;
   
   public void Initialize(ISubscriber<EnemyDiedMessage> enemyDiedSubscriber)
   {
      _subscriber = DisposableBag.Create(enemyDiedSubscriber.Subscribe(_ => UpdateCountDiedEnemies()));
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
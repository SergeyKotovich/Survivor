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

   private float _countingTime = 0.5f;
   
   private IDisposable _subscriber;
   
   public void Initialize(ISubscriber<EnemyDiedMessage> enemyDiedSubscriber, ISubscriber<BreakHasStartedMessage> breakHasStartedSubscriber)
   {
      _subscriber = DisposableBag.Create(breakHasStartedSubscriber.Subscribe(_=>ResetCounter()),
         enemyDiedSubscriber.Subscribe(_ => UpdateCountDiedEnemies()));
   }

   private void UpdateCountDiedEnemies()
   {
      Count++;
      _counter.text = Count.ToString();
   }

   private void ResetCounter()
   {
      StartCoroutine(ResetCounterCoroutines());
   }

   private IEnumerator ResetCounterCoroutines()
   {
      var currentTime = 0f;
      var startScore = Count;
      var newScore = 0f;
      while (currentTime<_countingTime)
      {
         Count = Mathf.Lerp(startScore, newScore, currentTime / _countingTime);
         currentTime += Time.deltaTime;
         _counter.text = Count.ToString("0");
         yield return null;
      }
     
   }
   private void OnDestroy()
   {
      _subscriber.Dispose();
   }
}
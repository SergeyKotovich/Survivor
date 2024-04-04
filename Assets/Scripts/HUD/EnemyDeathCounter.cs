using System;
using MessagePipe;
using TMPro;
using UnityEngine;
using VContainer;

public class EnemyDeathCounter : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI _counter;
   
   private IDisposable _subscriber;
   private int _count;

   [Inject]
   public void Construct(ISubscriber<EnemyDiedMessage> enemyDiedSubscriber)
   {
      _subscriber = enemyDiedSubscriber.Subscribe(_ => UpdateCountDiedEnemies());
   }

   private void UpdateCountDiedEnemies()
   {
      _count++;
      _counter.text = _count.ToString();
   }

   private void OnDestroy()
   {
      _subscriber.Dispose();
   }
}

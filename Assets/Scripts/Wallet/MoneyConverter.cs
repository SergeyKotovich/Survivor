using System;
using MessagePipe;
using UnityEngine;
using VContainer;

public class MoneyConverter : MonoBehaviour
{
    private const float _coefficient = 2f;
    private ICounter _enemyDeathCounter;
    private IPublisher<CountMoneyChangedMessage> _countMoneyChangedSPublisher;
    private IDisposable _subscriber;

    //[Inject]
  //  public void Construct( )
  //  {
  //      
  //  }

    public void Initialize(ICounter enemyDeathCounter, 
        IPublisher<CountMoneyChangedMessage> countMoneyChangedSPublisher,
        ISubscriber<BreakHasStartedMessage> breakHasStartedSubscriber)
    {
        _enemyDeathCounter = enemyDeathCounter;
        _countMoneyChangedSPublisher = countMoneyChangedSPublisher;
        _subscriber = breakHasStartedSubscriber.Subscribe(_ => ConvertMoney());
    }
    private void ConvertMoney()
    {
        var money = _enemyDeathCounter.Count * _coefficient;
        _countMoneyChangedSPublisher.Publish(new CountMoneyChangedMessage(money));
    }


    public void OnDestroy()
    {
        _subscriber.Dispose();
    }
}
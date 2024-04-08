using MessagePipe;
using UnityEngine;

public class MoneyConverter 
{
    private const float _coefficient = 2f;
    private ICounter _enemyDeathCounter;
    private IPublisher<CountMoneyChangedMessage> _countMoneyChangedSPublisher;

    public MoneyConverter(IPublisher<CountMoneyChangedMessage> countMoneyChangedSPublisher)
    {
        _countMoneyChangedSPublisher = countMoneyChangedSPublisher;
    }
    
    public void Initialize(ICounter enemyDeathCounter)
    {
        _enemyDeathCounter = enemyDeathCounter;
    }
    private void ConvertMoney()
    {
        var money = _enemyDeathCounter.Count * _coefficient;
        _countMoneyChangedSPublisher.Publish(new CountMoneyChangedMessage(money));
    }

    
}
using System;
using MessagePipe;
using UnityEngine;

public class Wallet : IDisposable
{
    public event Action<float> CountMoneyChanged;
    private float _currentCountMoney;
    private IDisposable _subscriber;

    public Wallet(ISubscriber<CountMoneyChangedMessage> countMoneyChangedSubscriber)
    {
        _subscriber = countMoneyChangedSubscriber.Subscribe(AddMoney);
    }
    private void AddMoney(CountMoneyChangedMessage countMoneyChanged)
    {
        _currentCountMoney += countMoneyChanged.Money;
        CountMoneyChanged?.Invoke(_currentCountMoney);
    }

    public bool TryBuy(float price)
    {
        if (_currentCountMoney>=price)
        {
            _currentCountMoney -= price;
            CountMoneyChanged?.Invoke(_currentCountMoney);
            return true;
        }
        Debug.Log("Недостаточно денег");
        return false;
    }

    public void Dispose()
    {
        _subscriber.Dispose();
    }
}
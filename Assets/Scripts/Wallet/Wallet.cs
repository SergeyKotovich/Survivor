using System;
using MessagePipe;
using UnityEngine;

public class Wallet : IDisposable
{
    public event Action<float> CountMoneyChanged;
    private float _currentCountMoney;
    private readonly IDisposable _subscriber;

    public Wallet(ISubscriber<MoneyCollectedMessage> moneyCollectedSubscriber)
    {
        _subscriber = moneyCollectedSubscriber.Subscribe(AddMoney);
    }
    private void AddMoney(MoneyCollectedMessage moneyCollected)
    {
        _currentCountMoney += moneyCollected.Money.CountMoney;
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
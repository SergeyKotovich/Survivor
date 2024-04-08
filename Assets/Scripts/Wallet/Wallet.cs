using System;
using MessagePipe;

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

    public void Dispose()
    {
        _subscriber.Dispose();
    }
}
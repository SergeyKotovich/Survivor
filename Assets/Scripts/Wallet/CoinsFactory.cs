using System;
using MessagePipe;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

public class CoinsFactory : IDisposable
{
    private float _shift = 0.2f;
    private readonly IObjectResolver _container;
    private readonly ObjectPool<Coin> _coinsPool;
    private readonly Coin _coinPrefab;
    private readonly IDisposable _subscriber;

    public CoinsFactory(IObjectResolver container, Coin coinPrefab,
        ISubscriber<MoneyCollectedMessage> moneyCollectedSubscriber)
    {
        _coinPrefab = coinPrefab;
        _container = container;
        _coinsPool = new ObjectPool<Coin>(Create, Get, Release);
        _subscriber = moneyCollectedSubscriber.Subscribe(OnMoneyCollected);
    }

    public void Spawn(Vector3 positionForSpawn)
    {
        var coin = _coinsPool.Get();
        positionForSpawn.y += _shift;
        coin.transform.position = positionForSpawn;
    }

    private Coin Create()
    {
        return _container.Instantiate(_coinPrefab);
    }

    private void Get(Coin coin)
    {
        coin.gameObject.SetActive(true);
    }

    private void Release(Coin coin)
    {
        coin.gameObject.SetActive(false);
    }
    private void OnMoneyCollected(MoneyCollectedMessage moneyCollected)
    {
        _coinsPool.Release(moneyCollected.Money);
    }


    public void Dispose()
    {
        _subscriber.Dispose();
    }
}
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

public class CoinsFactory
{
    private IObjectResolver _container;
    private ObjectPool<Coin> _coinsPool;
    private Coin _coinPrefab;

    public CoinsFactory(IObjectResolver container, Coin coinPrefab)
    {
        _coinPrefab = coinPrefab;
        _container = container;
        _coinsPool = new ObjectPool<Coin>(Create, Get, Release);
    }

    public Coin Spawn()
    {
        var coin = _coinsPool.Get();
        return coin;
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
}
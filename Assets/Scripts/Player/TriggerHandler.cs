using System;
using DG.Tweening;
using MessagePipe;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    private IFuel _inventory;
    private IPublisher<MoneyCollectedMessage> _moneyCollectedPublisher;

    public void Initialize(IFuel inventory, IPublisher<MoneyCollectedMessage> moneyCollectedPublisher)
    {
        _moneyCollectedPublisher = moneyCollectedPublisher;
        _inventory = inventory;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalConstants.BONFIRE_TAG))
        {
            if (_inventory.HasFuel())
            {
                var fuel = _inventory.GetFuel();
                var bonfire = other.GetComponent<Bonfire>();
                bonfire.TryAddFuel(fuel.AmountLight);
                fuel.transform.DOScale(0.5f, 0);
                fuel.transform.DOMove(other.transform.position, 1);
                fuel.transform.DOScale(0, 1);
            }
        }
        
        if (other.CompareTag(GlobalConstants.FIREWOOD_TAG) || other.CompareTag(GlobalConstants.SMALL_FIREWOOD_TAG))
        {
            if (!_inventory.IsFull())
            {
                var fuel = other.GetComponent<Fuel>();
                _inventory.AddFuel(fuel);
                other.transform.DOMove(transform.position, 0.2f);
                other.transform.DOScale(0, 0.2f);
                other.transform.SetParent(transform);
            }
        }

        if (other.CompareTag(GlobalConstants.COIN_TAG))
        {
           var coin = other.GetComponent<Coin>();
            _moneyCollectedPublisher.Publish(new MoneyCollectedMessage(coin));
        }
    }
}
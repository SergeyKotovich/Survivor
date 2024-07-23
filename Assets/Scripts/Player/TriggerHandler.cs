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
        if (other.CompareTag(GlobalConstants.COIN_TAG))
        {
           var coin = other.GetComponent<Coin>();
            _moneyCollectedPublisher.Publish(new MoneyCollectedMessage(coin));
        }
    }
}
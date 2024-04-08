using System;
using UnityEngine;
using VContainer;

public class UIController : MonoBehaviour
{
    [SerializeField] private EnemyDeathCounter _enemyDeathCounter;
    [SerializeField] private WalletView _walletView;
    private MoneyConverter _moneyConverter;
    private Wallet _wallet;

    [Inject]
    private void Construct(MoneyConverter moneyConverter, Wallet wallet)
    {
        _wallet = wallet;
        _moneyConverter = moneyConverter;
        _moneyConverter.Initialize(_enemyDeathCounter);
        _walletView.Initialize(_wallet);
    }
}
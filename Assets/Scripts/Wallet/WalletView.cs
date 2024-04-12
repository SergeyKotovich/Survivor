using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countMoney;
    [SerializeField] private float _calculatingTime = 0.5f;
    private float _currentCountMoney;
    private Wallet _wallet;

    public void Initialize(Wallet wallet)
    {
        _wallet = wallet;
        _wallet.CountMoneyChanged += UpdateCountMoney;
    }

    private void UpdateCountMoney(float newCountMoney)
    {
        StartCoroutine(UpdateCountMoneyCoroutine(newCountMoney));
    }

    private IEnumerator UpdateCountMoneyCoroutine(float newCountMoney)
    {
        var currentTime = 0f;
        while (currentTime<_calculatingTime)
        {
            _currentCountMoney = Mathf.Lerp(_currentCountMoney, newCountMoney, currentTime / _calculatingTime);
            currentTime += Time.deltaTime;
            _countMoney.text = _currentCountMoney.ToString("0");
            yield return null;
        }
    }

    private void OnDestroy()
    {
        _wallet.CountMoneyChanged -= UpdateCountMoney;
    }
}
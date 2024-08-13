using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countMoney;
    [SerializeField] private float _calculatingTime = 0.5f;
    [SerializeField] private TextMeshProUGUI _messageNotEnoughMoney;
    private float _currentCountMoney;
    private Wallet _wallet;
    
    public void Initialize(Wallet wallet)
    {
        _wallet = wallet;
        _wallet.CountMoneyChanged += UpdateCountMoney;
        _wallet.OnNotEnoughMoney += ShowMessageOnNotEnoughMoney;
    }

    private void ShowMessageOnNotEnoughMoney()
    {
        _messageNotEnoughMoney.DOFade(1, 0.1f);
        DOVirtual.DelayedCall(1f, () => _messageNotEnoughMoney.DOFade(0, 0.1f));
    }

    private void UpdateCountMoney(float newCountMoney)
    {
        StartCoroutine(UpdateCountMoneyCoroutine(newCountMoney));
    }

    private IEnumerator UpdateCountMoneyCoroutine(float newCountMoney)
    {
        var currentTime = 0f;
        while (currentTime < _calculatingTime)
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
        _wallet.OnNotEnoughMoney -= ShowMessageOnNotEnoughMoney;
    }
}
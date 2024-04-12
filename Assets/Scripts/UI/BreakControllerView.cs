using System;
using System.Collections;
using MessagePipe;
using TMPro;
using UnityEngine;

public class BreakControllerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeLabel;
    [SerializeField] private float _breakTime = 10;
    [SerializeField] private float _delayTime = 1;
    private IDisposable _subscriber;
    
    public void Initialize(ISubscriber<BreakHasStartedMessage> breakHasStartedSubscriber)
    {
        _subscriber = breakHasStartedSubscriber.Subscribe(_ => StartBreak());
    }

    private void StartBreak()
    {
        _timeLabel.text = _breakTime.ToString();
        StartCoroutine(StartBreakCoroutine());
    }

    private IEnumerator StartBreakCoroutine()
    {
        var currentTime = _breakTime;
        while (currentTime>=0)
        {
            _timeLabel.text = currentTime.ToString();
            yield return new WaitForSeconds(_delayTime);
            currentTime--;
        }
    }

    private void OnDestroy()
    {
        _subscriber.Dispose();
    }
}
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
    private IPublisher<BreakFinishedMessage> _breakFinishedPublisher;

    public void Initialize(ISubscriber<AllEnemiesDiedMessage>allEnemiesDiedSubscriber,
        IPublisher<BreakFinishedMessage> breakFinishedMessagePublisher)
    {
        _breakFinishedPublisher = breakFinishedMessagePublisher;
        _subscriber = allEnemiesDiedSubscriber.Subscribe(_ => StartBreak());
    }

    private void StartBreak()
    {
        gameObject.SetActive(true);
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
        _breakFinishedPublisher.Publish(new BreakFinishedMessage());
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _subscriber.Dispose();
    }
}
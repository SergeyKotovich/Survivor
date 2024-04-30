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
    private int _breakCounter;
    private Action _npcMessagesCallback;

    public void Initialize(ISubscriber<AllEnemiesDiedMessage>allEnemiesDiedSubscriber,
        IPublisher<BreakFinishedMessage> breakFinishedMessagePublisher, Action NPCMessagesCallback)
    {
        _npcMessagesCallback = NPCMessagesCallback;
        _breakFinishedPublisher = breakFinishedMessagePublisher;
        _subscriber = allEnemiesDiedSubscriber.Subscribe(_ => StartBreak());
    }

    private void StartBreak()
    {
        _breakCounter++;
        gameObject.SetActive(true);
        _timeLabel.text = _breakTime.ToString();
        StartCoroutine(StartBreakCoroutine());
        if (_breakCounter == Decimal.One)
        {
            _npcMessagesCallback?.Invoke();
        }
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
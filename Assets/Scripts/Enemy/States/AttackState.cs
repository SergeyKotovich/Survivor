using System;
using MessagePipe;
using UnityEngine;
using VContainer;

public class AttackState : MonoBehaviour, IState
{
    [SerializeField] private EnemyAnimationController _animationController;
    [SerializeField] private EnemyTargetController _enemyTargetController;

    private StateMachine _stateMachine;

    private IDisposable _subscriber;
    private SoundsManager _soundsManager;

    [Inject]
    public void Construct(ISubscriber<PlayerDiedMessage> playerDiedSubscriber, SoundsManager soundsManager)
    {
        _soundsManager = soundsManager;
        _subscriber = playerDiedSubscriber.Subscribe(_ => EnterWaitingState());
    }

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _enemyTargetController.TargetFar += EnterMoveToTargetState;
    }

    private void EnterMoveToTargetState()
    {
        _animationController.StopAttack();
        _stateMachine.Enter<MoveToTargetState>();
    }

    private void EnterWaitingState()
    {
        _enemyTargetController.ResetTarget();
        _stateMachine.Enter<WaitingState>();
    }

    public void OnEnter()
    {
        _enemyTargetController.ResetTarget();
        _animationController.ShowAttack();
        _soundsManager.PlayZombieAttack();
    }

    private void OnDestroy()
    {
        _enemyTargetController.TargetFar -= EnterMoveToTargetState;
        _subscriber.Dispose();
    }
}
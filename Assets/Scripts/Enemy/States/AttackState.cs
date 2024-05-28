using System;
using MessagePipe;
using UnityEditor.Animations;
using UnityEngine;
using VContainer;

public class AttackState : MonoBehaviour, IState
{
    [SerializeField] private EnemyAnimationController _animationController;
    [SerializeField] private EnemyTargetController _enemyTargetController;
    
    private StateMachine _stateMachine;
    
    private IDisposable _subscriber;

    [Inject]
    public void Construct(ISubscriber<PlayerDiedMessage> playerDiedSubscriber)
    {
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
    }

    private void OnDestroy()
    {
        _enemyTargetController.TargetFar -= EnterMoveToTargetState;
        _subscriber.Dispose();
    }
}
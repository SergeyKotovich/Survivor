using System;
using MessagePipe;
using UnityEngine;
using VContainer;

public class MoveToTargetState : MonoBehaviour, IState
{
    [SerializeField] private EnemyTargetController _enemyTargetController;
    [SerializeField] private EnemyAnimationController _animationController;

    private StateMachine _stateMachine;
    private IMovable _target;
    private IDisposable _subscriber;

    private Collider _collider;
    private Rigidbody _rigidbody;

    [Inject]
    public void Construct(IMovable target, ISubscriber<PlayerDiedMessage> playerDiedSubscriber)
    {
        _target = target;
        _subscriber = playerDiedSubscriber.Subscribe(_ => EnterWaitingState());
    }

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _enemyTargetController.TargetNearby += EnterAttackState;
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnEnter()
    {
        _enemyTargetController.SetTarget(_target);
        _animationController.ShowWalking();
        _collider.enabled = true;
        _rigidbody.isKinematic = false;
    }

    private void EnterAttackState()
    {
        _stateMachine.Enter<AttackState>();
    }

    private void EnterWaitingState()
    {
        _enemyTargetController.ResetTarget();
        _stateMachine.Enter<WaitingState>();
    }

    private void OnDestroy()
    {
        _enemyTargetController.TargetNearby -= EnterAttackState;
        _subscriber.Dispose();
    }
}
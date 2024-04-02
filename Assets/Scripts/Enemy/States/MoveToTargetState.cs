using System;
using MessagePipe;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

public class MoveToTargetState : MonoBehaviour, IState
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private EnemyAnimationController _animationController;
    private StateMachine _stateMachine;
    private IMovable _target;
    private bool _isActive;
    private IDisposable _subscriber;

    [Inject]
    public void Construct(IMovable movable, ISubscriber<EnemyDiedMessage> enemyDiedSubscriber)
    {
        _target = movable;
        _subscriber = enemyDiedSubscriber.Subscribe(_ =>EnterDeathState());
    }
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _animationController.ShowWalking();
        _isActive = true;
    }

    private void Update()
    {
        if (_isActive)
        {
            _navMeshAgent.destination = _target.Position;
            if (Vector3.Distance(transform.position,_target.Position)<3f)
            {
                _stateMachine.Enter<AttackState>();
            }

            if (Vector3.Distance(transform.position,_target.Position)>3f)
            {
                _animationController.ShowWalking();
            }
        }
    }

    private void EnterDeathState()
    {
        _isActive = false;
        _stateMachine.Enter<DeathState>();
    }

    private void OnDestroy()
    {
        _subscriber.Dispose();
    }
}
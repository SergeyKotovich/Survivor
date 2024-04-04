using System;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

public class MoveToTargetState : MonoBehaviour, IState
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private EnemyAnimationController _animationController;
    [SerializeField] private float _minDistance = 3f;
    
    private ICollision _enemyCollisionHandler;
    private StateMachine _stateMachine;
    private IMovable _target;
    private bool _isActive;
    
    [Inject]
    public void Construct(IMovable movable)
    {
        _target = movable;
        _enemyCollisionHandler = GetComponent<ICollision>();
        _enemyCollisionHandler.EnemyDied += EnterDeathState;
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
            if (Vector3.Distance(transform.position,_target.Position)<_minDistance)
            {
                _stateMachine.Enter<AttackState>();
                return;
            }

            if (Vector3.Distance(transform.position,_target.Position)>_minDistance)
            {
                _animationController.ShowWalking();
            }
        }

        if (!_isActive)
        {
            _navMeshAgent.destination = transform.position;
        }
    }

    private void EnterDeathState()
    {
        _isActive = false;
        _stateMachine.Enter<DeathState>();
    }

    private void OnDestroy()
    {
        _enemyCollisionHandler.EnemyDied += EnterDeathState;
    }
}
using System;
using MessagePipe;
using UnityEngine;
using VContainer;

public class DeathState : MonoBehaviour, IState
{
    [SerializeField] private EnemyAnimationController _animationController;
    [SerializeField] private EnemyTargetController _enemyTargetController;

    private Rigidbody _rigidbody;
    private Collider _collider;
    private StateMachine _stateMachine;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _enemyTargetController.ResetTarget();
        _rigidbody.isKinematic = true;
        _collider.enabled = false;
        _animationController.ShowDeath();
    }

   
}
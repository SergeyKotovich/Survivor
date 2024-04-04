using System;
using UnityEngine;

public class DeathState : MonoBehaviour, IState
{
    [SerializeField] private EnemyAnimationController _animationController;
    private StateMachine _stateMachine;
    
    private Rigidbody _rigidbody;
    private Collider _collider;
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
        _rigidbody.isKinematic = true;
        _collider.enabled = false;
        _animationController.ShowDeath();
    }

   
}
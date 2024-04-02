using System;
using UnityEngine;

public class DeathState : MonoBehaviour, IState
{
    [SerializeField] private EnemyAnimationController _animationController;
    private StateMachine _stateMachine;

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _animationController.ShowDeath();
    }

   
}
using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _heath;
    private int _speed;
    private int _damage;

    
    private void Start()
    {
        var stateMachine = new StateMachine
        (
            GetComponent<WaitingState>(),
            GetComponent<MoveToTargetState>(),
            GetComponent<AttackState>(),
            GetComponent<DeathState>()
        );
        stateMachine.Initialize();
        stateMachine.Enter<WaitingState>();
    }
    public void Initialize(EnemyConfig enemyConfig)
    {
        _heath = enemyConfig.Health;
        _speed = enemyConfig.Speed;
        _damage = enemyConfig.Damage;
    }
}
using System;
using MessagePipe;
using UnityEngine;
using VContainer;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyAttackController _enemyAttackController;
    [SerializeField] private EnemyCollisionHandler _enemyCollisionHandler;
    [SerializeField] private EnemyTargetController _enemyTargetController;
    
    private HealthController _healthController;
    private StateMachine _stateMachine;
    private IPublisher<EnemyDiedMessage> _enemyDiedPublisher;

    [Inject]
    private void Construct(IPublisher<EnemyDiedMessage> enemyDiedPublisher)
    {
        _enemyDiedPublisher = enemyDiedPublisher;
    }

    private void Start()
    {
        _stateMachine = new StateMachine
        (
            GetComponent<WaitingState>(),
            GetComponent<MoveToTargetState>(),
            GetComponent<AttackState>(),
            GetComponent<DeathState>()
        );
        _stateMachine.Initialize();
        _stateMachine.Enter<MoveToTargetState>();
    }
    public void Initialize(EnemyConfig enemyConfig)
    {
        _healthController = new HealthController(enemyConfig.Health);
        _enemyCollisionHandler.DamageReceived += TakeDamage;
        _healthController.Died += EnterDeathState;
        _enemyAttackController.Initialize(enemyConfig.Damage);
        _enemyTargetController.Initialize(enemyConfig.Speed);
    }

    private void TakeDamage(float damage)
    {
        _healthController.TakeDamage(damage);
    }

    private void EnterDeathState()
    {
        _enemyDiedPublisher.Publish(new EnemyDiedMessage(this));
        _stateMachine.Enter<DeathState>();
    }
}
using System;
using MessagePipe;
using UnityEngine;
using UnityEngine.Assertions;

public class HealthController : IHealthHandler, IHealth
{
    public event Action HealthChanged;
    public event Action Died;
    public float Health => _health;
    public float MaxHealth => _maxHealth;

    private float _health;
    private readonly float _maxHealth;
    private bool _isDead => Health <= 0;

    public HealthController(float health)
    {
        _health = health;
        _maxHealth = _health;
    }

    public void TakeDamage(float damage)
    {
        Assert.IsFalse(_isDead , " You are trying to deal damage to a died Character");
        if (_isDead)
        {
            return;
        }
        if (damage >= _health)
        {
            _health = 0;
        }
        else
        {
            _health -= damage;
        }

        HealthChanged?.Invoke();
        if (_health == 0)
        {
            Died?.Invoke();
        }
    }

    public void Heal()
    {
        _health = _maxHealth;
        HealthChanged?.Invoke();
    }
}
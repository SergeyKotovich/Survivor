using System;

public interface IHealth
{
    public event Action HealthChanged;
    public event Action Died;
    public float Health { get; }
    public float MaxHealth { get; }
}
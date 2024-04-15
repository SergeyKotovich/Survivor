using System;

public interface IAtackImprovable
{
    public event Action<float, float> AttackSpeedUpdated; 
    public event Action<float, float> AttackRangeUpdated;
}
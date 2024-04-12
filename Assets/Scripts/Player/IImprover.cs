using System;

public interface IImprover
{
    public event Action<float, float> AttackSpeedUpdated; 
    public event Action<float, float> AttackRangeUpdated;
}
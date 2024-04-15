using System;

public interface ISpeedImprovable
{
    public event Action<float, float> RunningSpeedUpdated; 
}
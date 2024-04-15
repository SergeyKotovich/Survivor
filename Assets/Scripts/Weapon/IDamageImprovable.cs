using System;

public interface IDamageImprovable
{
    public event Action<float, float> DamageUpdated;
}
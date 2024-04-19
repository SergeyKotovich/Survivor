using System;

public interface IPrice
{
    public event Action<int> AttackSpeedImproved;
    public event Action<int> AttackRangeImproved;
    public event Action<int> RunningSpeedImproved;
    public event Action<int> DamageImproved;
    public event Action<int> HealImproved;
}
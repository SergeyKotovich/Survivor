using System.Collections.Generic;
using UnityEngine;

public interface IFuel
{
    public bool HasFuel();
    public Fuel GetFuel();
    public void AddFuel(Fuel fuel);
    public bool IsFull();
}
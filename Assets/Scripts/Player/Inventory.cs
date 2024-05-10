using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Inventory : MonoBehaviour, IFuel
{
    [SerializeField] private int _countAvailablePlace = 2;
    private Queue<Fuel> _fuel = new();
    
    public bool HasFuel()
    {
        return _fuel.Count!=0;
    }

    public Fuel GetFuel()
    {
        return _fuel.Dequeue();
    }

    public void AddFuel(Fuel fuel)
    {
        if (_fuel.Count < _countAvailablePlace)
        {
            _fuel.Enqueue(fuel);
        }
    }

    public bool IsFull()
    {
        if (_fuel.Count==_countAvailablePlace)
        {
            return true;
        }
        return false;
    }
}
using System;
using System.Collections.Generic;

public class StateMachine
{
    private Dictionary<Type, IInitializable> _states = new();
    
    
    public StateMachine(params IInitializable [] states)
    {
        foreach (var state in states)
        {
           _states.Add(state.GetType(),state); 
        }
    }

    public void Initialize()
    {
        foreach (var statePair in _states)
        {
            statePair.Value.Initialize(this);
        }
    }

    public void Enter<TState>() where TState : class, IState
    {
        var currentState = (IState)_states[typeof(TState)];
        currentState.OnEnter();
    }
}
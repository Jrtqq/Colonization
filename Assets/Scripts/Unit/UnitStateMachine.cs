using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class UnitStateMachine : IStateSwitcher
{
    private State[] _states;
    private State _currenState;
    private UnitStateData _data;

    public UnitStateMachine(Unit unit)
    {
        _data = new();

        _states = new State[] 
        {
            new Rest(unit, _data, this),
            new GoToBox(unit, _data, this),
            new GoToBase(unit, _data, this),
            new SetNewBase(unit, _data, this)
        };

        _currenState = _states[0];
        _currenState.Enter();
    }

    public void Update() => _currenState.Update();

    public void SwitchState<Behaviour>() where Behaviour : global::State
    {
        _currenState.Exit();
        _currenState = _states.Where(x => x is Behaviour).FirstOrDefault();
        _currenState.Enter();
    }

    public void SetBox(Box box) => _data.Box = box;

    public void SetNewBasePosition(Vector3 position) => _data.NewBasePosition = position;

    public Type CheckCurrentState() => _currenState.GetType();
}

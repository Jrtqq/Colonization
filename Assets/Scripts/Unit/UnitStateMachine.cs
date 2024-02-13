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

    public UnitStateMachine(Unit unit, Vector3 basePosition)
    {
        _data = new(basePosition);

        _states = new State[] 
        {
            new Rest(unit, _data, this),
            new GoToBox(unit, _data, this),
            new GoToBase(unit, _data, this)
        };

        _currenState = _states[0];
        _currenState.Enter();
    }

    public void Update()
    {
        _currenState.Update();
    }

    public void SwitchState<Behaviour>() where Behaviour : global::State
    {
        _currenState.Exit();
        _currenState = _states.Where(x => x is Behaviour).FirstOrDefault();
        _currenState.Enter();
    }

    public void SetBox(Box box)
    {
        Debug.Log("устанавливаю коробку");
        _data.Box = box;
        Debug.Log(_data.Box);
    }

    public void SetBasePosition(Vector3 basePosition) => _data.BasePosition = basePosition;

    public Type CheckCurrentState() => _currenState.GetType();
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class UnitBehaviourManager : IStateSwitcher
{
    private Behaviour[] _behaviours;
    private Behaviour _currentBehaviour;
    private UnitBehaviourData _data;

    public UnitBehaviourManager(Unit unit, Vector3 basePosition)
    {
        _data = new(basePosition);

        _behaviours = new Behaviour[] 
        {
            new Rest(unit, _data, this),
            new GoToBox(unit, _data, this),
            new GoToBase(unit, _data, this)
        };

        _currentBehaviour = _behaviours[0];
        _currentBehaviour.Enter();
    }

    public void Update()
    {
        _currentBehaviour.Update();
    }

    public void SwitchState<Behaviour>() where Behaviour : global::Behaviour
    {
        _currentBehaviour.Exit();
        _currentBehaviour = _behaviours.Where(x => x is Behaviour).FirstOrDefault();
        _currentBehaviour.Enter();
    }

    public void SetBox(Box box)
    {
        Debug.Log("устанавливаю коробку");
        _data.Box = box;
    }
    public void SetBasePosition(Vector3 basePosition) => _data.BasePosition = basePosition;

    public Type CheckCurrentState() => _currentBehaviour.GetType();
}

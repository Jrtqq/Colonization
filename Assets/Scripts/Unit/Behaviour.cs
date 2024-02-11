using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behaviour
{
    protected Unit Unit;
    protected UnitBehaviourData Data;
    protected IStateSwitcher StateSwitcher;

    public Behaviour(Unit unit, UnitBehaviourData data, IStateSwitcher stateSwitcher)
    {
        Unit = unit;
        Data = data;
        StateSwitcher = stateSwitcher;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
}

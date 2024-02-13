using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected Unit Unit;
    protected UnitStateData Data;
    protected IStateSwitcher StateSwitcher;

    public State(Unit unit, UnitStateData data, IStateSwitcher stateSwitcher)
    {
        Unit = unit;
        Data = data;
        StateSwitcher = stateSwitcher;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
}

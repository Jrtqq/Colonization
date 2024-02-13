using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : State
{
    public Rest(Unit unit, UnitStateData data, IStateSwitcher stateSwitcher) : base(unit, data, stateSwitcher) { }

    public override void Enter() { }

    public override void Exit() { }

    public override void Update() 
    {
        Debug.Log(Data.Box);

        if (Data.Box != null)
        {
            StateSwitcher.SwitchState<GoToBox>();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : Behaviour
{
    public Rest(Unit unit, UnitBehaviourData data, IStateSwitcher stateSwitcher) : base(unit, data, stateSwitcher) { }

    public override void Enter() { }

    public override void Exit() { }

    public override void Update() 
    {
        Debug.Log(Data.Box);

        if (Data.Box != null)
        {
            StateSwitcher.SwitchState<GoToBox>();
            Debug.Log("пытаюсь сменить состояние");
        }
    }
}

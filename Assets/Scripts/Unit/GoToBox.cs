using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoToBox : State
{
    private float _endDistance = 1.5f;

    private Vector3 UnitPosition => Unit.transform.position;
    private Vector3 BoxPosition => Data.Box.transform.position;

    public GoToBox(Unit unit, UnitStateData data, IStateSwitcher stateSwitcher) : base(unit, data, stateSwitcher) { }

    public override void Enter() { }

    public override void Exit() { }

    public override void Update()
    {
        if (Vector3.Distance(UnitPosition, BoxPosition) <= _endDistance)
        {
            StateSwitcher.SwitchState<GoToBase>();
            return;
        }

        Vector3 target = new(BoxPosition.x, UnitPosition.y, BoxPosition.z);

        Unit.transform.position = Vector3.MoveTowards(UnitPosition, target, Data.Speed * Time.deltaTime);
    }
}

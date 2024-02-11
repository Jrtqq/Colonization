using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GoToBase : Behaviour
{
    private float _endDistance = 2;

    private Transform UnitTransform => Unit.transform;
    private Transform BoxTransform => Data.Box.transform;
    private Vector3 BasePosition => Data.BasePosition;

    public GoToBase(Unit unit, UnitBehaviourData data, IStateSwitcher stateSwitcher) : base(unit, data, stateSwitcher) { }

    public override void Enter()
    {
        BoxTransform.SetParent(UnitTransform);
    }

    public override void Exit()
    {
        BoxTransform.SetParent(null);
        Data.Box = null;
    }

    public override void Update()
    {
        if (Vector3.Distance(UnitTransform.position, BasePosition) <= _endDistance)
        {
            Unit.BoxDelivered?.Invoke(Data.Box);
            StateSwitcher.SwitchState<Rest>();
            return;
        }

        Vector3 target = new(BasePosition.x, UnitTransform.position.y, BasePosition.z);

        UnitTransform.position = Vector3.MoveTowards(UnitTransform.position, target, Data.Speed * Time.deltaTime);
    }
}

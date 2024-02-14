using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetNewBase : State
{
    private BaseInstantiator _baseCreator = new();

    public SetNewBase(Unit unit, UnitStateData data, IStateSwitcher stateSwitcher) : base(unit, data, stateSwitcher) { }

    private Vector3 UnitPosition => Unit.transform.position;
    private Vector3 Target => Data.NewBasePosition;

    public override void Enter() { }

    public override void Exit()
    {
        _baseCreator.Spawn(Target, Unit);
        Unit.SetNewBase(Target);

        Data.NewBasePosition = Config.NullVector;
    }

    public override void Update()
    {
        if (UnitPosition.x == Target.x && UnitPosition.z == Target.z)
        {
            StateSwitcher.SwitchState<Rest>();
            return;
        }

        Vector3 target = new(Target.x, UnitPosition.y, Target.z);

        Unit.transform.position = Vector3.MoveTowards(UnitPosition, target, Data.Speed * Time.deltaTime);
    }
}

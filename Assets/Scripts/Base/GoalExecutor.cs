using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoalExecutor
{
    private InstantiateGoal[] _goals;
    private InstantiateGoal _currentGoal;

    public int CurrentPrice => _currentGoal.Price;

    public GoalExecutor()
    {
        Unit unit = Resources.Load<Unit>("Prefabs/Unit");
        Base @base = Resources.Load<Base>("Prefabs/Base");

        _goals = new InstantiateGoal[]
        {
            new InstantiateGoal(unit, 3),
            new InstantiateGoal(@base, 5)
        };

        _currentGoal = _goals[0];
    }

    public void TryExecute(Box[] cost, GameObject instantiator, Vector3 instantiatePosition)
    {
        _currentGoal.Execute(instantiator, instantiatePosition);
    }

    public void SwitchGoal<Goal>() where Goal : IInitiable
    {
        _currentGoal = _goals.Where(x => x is Goal).FirstOrDefault();
    }
}

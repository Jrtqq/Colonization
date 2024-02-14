using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreatorExecutor
{
    private ICreator[] _creators;
    private ICreator _currentCreator;

    public int CurrentPrice => _currentCreator.Price;

    public CreatorExecutor()
    {
        _creators = new ICreator[]
        {
            new UnitCreator(),
            new BaseCreator()
        };

        _currentCreator = _creators[0];
    }

    public bool TryExecute(Box[] cost, Base instantiator, Vector3 instantiatePosition)
    {
        if (cost.Length > CurrentPrice)
            return false;

        foreach(Box box in cost)
            Object.Destroy(box.gameObject);

        _currentCreator.Create(instantiatePosition, instantiator);
        return true;
    }

    public void SwitchPriority<Priority>()
    {
        _currentCreator = _creators.Where(x => x is Priority).FirstOrDefault();
    }
}

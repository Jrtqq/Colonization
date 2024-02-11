using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateGoal
{
    private IInitiable _prefab;

    public int Price { get; private set; } 

    public InstantiateGoal(IInitiable prefab, int price)
    {
        _prefab = prefab;
        Price = price;
    }

    public IInitiable Execute(GameObject instantiator, Vector3 InstantiatePosition)
    {
        _prefab.Init(instantiator, InstantiatePosition);
        return _prefab;
    }
}

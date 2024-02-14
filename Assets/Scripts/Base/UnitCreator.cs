using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCreator : ICreator
{
    private Unit _prefab;
    private int _price = 3;

    public UnitCreator() => _prefab = Resources.Load<Unit>("Prefabs/Unit");

    int ICreator.Price => _price;

    public void Create(Vector3 position, Base instantiator)
    {
        Unit unit = Object.Instantiate(_prefab, position, Quaternion.identity);
        unit.Init(instantiator.transform.position);

        instantiator.AddUnit(unit);
    }
}

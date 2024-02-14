using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCreator : ICreator
{
    private int _price = 5;

    public int Price => _price;

    public void Create(Vector3 position, Base instantiator)
    {
        instantiator.SetNewBaseReady();
    }
}

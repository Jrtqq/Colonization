using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICreator
{
    public int Price { get; }

    void Create(Vector3 position, Base instantiator);
}

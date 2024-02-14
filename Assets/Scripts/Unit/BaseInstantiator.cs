using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInstantiator
{
    private Base _prefab;

    public BaseInstantiator() => _prefab = Resources.Load<Base>("Prefabs/Base");

    public void Spawn(Vector3 position, Unit startUnit)
    {
        Vector3 spawnPosition = new(position.x, Config.BasePositionY, position.z);
        Object.Instantiate(_prefab, spawnPosition, Quaternion.identity).Init(startUnit);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitBehaviourData
{
    public readonly float Speed = 6;

    public Vector3 BasePosition;
    public Box Box = null;

    public UnitBehaviourData(Vector3 basePosition) => BasePosition = basePosition;
}

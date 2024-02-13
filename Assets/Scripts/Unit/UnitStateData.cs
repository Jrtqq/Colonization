using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitStateData
{
    public readonly float Speed = 6;

    public Vector3 BasePosition;
    public Box Box = null;

    public UnitStateData(Vector3 basePosition) => BasePosition = basePosition;
}

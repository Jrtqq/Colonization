using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    public UnityAction<Box> BoxDelivered;

    private UnitStateMachine _stateMachine;

    public Vector3 BasePosition { get; private set; } = new(0, Config.BasePositionY, 0);

    public bool IsBusy => _stateMachine.CheckCurrentState() != typeof(Rest);

    private void Awake()
    {
        _stateMachine ??= new UnitStateMachine(this);
    }

    private void Update()
    { 
        _stateMachine.Update(); 
    }

    public void GoToBox(Box box) => _stateMachine.SetBox(box);

    public void GoToNewBase(Vector3 position) => _stateMachine.SetNewBasePosition(position);

    public void SetNewBase(Vector3 position) => BasePosition = position;

    public void Init(Vector3 basePosition)
    {
        _stateMachine ??= new UnitStateMachine(this);
        BasePosition = basePosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour, IInitiable
{
    public UnityAction<Box> BoxDelivered;

    private UnitStateMachine _stateMachine;
    private Vector3 _basePosition = new(0, 0.1f, 0);

    public bool IsBusy => _stateMachine.CheckCurrentState() != typeof(Rest);

    private void Awake()
    {
        _stateMachine ??= new UnitStateMachine(this, _basePosition);
    }

    private void Update()
    {
        _stateMachine.Update();

        Debug.Log(_stateMachine.CheckCurrentState());
    }

    public void GoToBox(Box box)
    {
        _stateMachine.SetBox(box);
    }

    public void Init(GameObject @base, Vector3 instantiatePosition)
    {
        if (@base.TryGetComponent(out Base baseComponent) == false)
            throw new System.Exception();
        else
            baseComponent.AddUnit(this);

        _stateMachine ??= new UnitStateMachine(this, _basePosition);
        _basePosition = @base.transform.position;

        Instantiate(this, instantiatePosition, Quaternion.identity);

        _stateMachine.SetBasePosition(@base.transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour, IInitiable
{
    public UnityAction<Box> BoxDelivered;

    private UnitBehaviourManager _behaviourManager;
    private Vector3 _basePosition = new(0, 0.1f, 0);

    public bool IsBusy => _behaviourManager.CheckCurrentState() != typeof(Rest);

    private void Awake()
    {
        _behaviourManager ??= new UnitBehaviourManager(this, _basePosition);
    }

    private void Update()
    {
        _behaviourManager.Update();

        Debug.Log(_behaviourManager.CheckCurrentState());
    }

    public void GoToBox(Box box)
    {
        Debug.Log("передаю коробку в машину состояний");
        _behaviourManager.SetBox(box);
    }

    public void Init(GameObject @base, Vector3 instantiatePosition)
    {
        if (@base.TryGetComponent(out Base baseComponent) == false)
            throw new System.Exception();
        else
            baseComponent.AddUnit(this);

        _behaviourManager ??= new UnitBehaviourManager(this, _basePosition);
        _basePosition = @base.transform.position;

        Instantiate(this, instantiatePosition, Quaternion.identity);

        _behaviourManager.SetBasePosition(@base.transform.position);
    }
}

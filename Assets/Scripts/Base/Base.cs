using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour, IInitiable
{
    [SerializeField] private List<Unit> _units;

    private List<Box> _deliveredBoxes = new();

    private BoxScanner _scanner;
    private GoalExecutor _goalExecutor;

    private Vector3 UnitSpawnPosition => new(transform.position.x, transform.position.y + 1, transform.position.z);

    private void Awake()
    {
        _scanner = new(transform.position);
        _goalExecutor = new GoalExecutor();
    }

    private void OnEnable() 
    {
        for (int i = 0; i < _units.Count; i++)
        {
            _units[i].BoxDelivered += AddBox;
        }

        _scanner.BoxFound += OnBoxFound;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _units.Count; i++)
        {
            _units[i].BoxDelivered -= AddBox;
        }

        _scanner.BoxFound -= OnBoxFound;
    }

    private void Update()
    {
        _scanner.Update();
    }

    private void OnBoxFound(Box box)
    {
        if (TryGetFreeUnit(out Unit unit))
        {
            Debug.Log("нашёл свободного юнита");
            box.Mark();
            unit.GoToBox(box);
        }
    }

    private bool TryGetFreeUnit(out Unit unit)
    {
        unit = null;

        for (int i = 0; i < _units.Count; i++)
        {
            if (_units[i].IsBusy == false)
            {
                unit = _units[i];
                return true;
            }
        }

        return false;
    }

    private void AddBox(Box box)
    {
        _deliveredBoxes.Add(box);

        _goalExecutor.TryExecute(_deliveredBoxes.Take(_goalExecutor.CurrentPrice).ToArray(), gameObject, UnitSpawnPosition);
    }

    public void Init(GameObject instantiator, Vector3 instantiatePosition)
    {
        if (instantiator.TryGetComponent(out Unit unit) == false)
            throw new System.Exception();
        else
        {
            AddUnit(unit);
            Instantiate(this, instantiatePosition, Quaternion.identity);
        }
    }

    public void AddUnit(Unit unit) => _units.Add(unit);
}

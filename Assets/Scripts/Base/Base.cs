using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private List<Unit> _units = new();

    private List<Box> _deliveredBoxes = new();

    private BaseData _data = new();
    private BoxScanner _scanner;
    private CreatorExecutor _creatorExecutor;

    private Vector3 UnitSpawnPosition => new(transform.position.x, transform.position.y + 1, transform.position.z);

    public bool IsNewBaseReady { get; private set; }

    private void Awake()
    {
        _scanner = new(transform.position);
        _creatorExecutor = new CreatorExecutor();
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
        if (IsNewBaseReady)
            TrySendUnitToNewBase();

        _scanner.Update();
    }

    public void Init(Unit startUnit) => AddUnit(startUnit);

    public void SetNewBaseReady() => IsNewBaseReady = true;

    public void SetFlag(Flag flag)
    {
        if (_data.NewBaseFlag != null)
            Destroy(_data.NewBaseFlag.gameObject);

        _data.NewBaseFlag = flag;
        _creatorExecutor.SwitchPriority<BaseCreator>();
    }

    public void AddUnit(Unit unit)
    {
        _units.Add(unit);
        unit.BoxDelivered += AddBox;
    }

    private void OnBoxFound(Box box)
    {
        if (TryGetFreeUnit(out Unit unit))
        {
            box.Mark();
            unit.GoToBox(box);
        }
    }

    private void TrySendUnitToNewBase()
    {
        if (TryGetFreeUnit(out Unit unit))
        {
            unit.GoToNewBase(_data.NewBaseFlag.transform.position);
            _units.Remove(unit);
            unit.BoxDelivered -= AddBox;

            Destroy(_data.NewBaseFlag.gameObject);
            _data.NewBaseFlag = null;

            IsNewBaseReady = false;
            _creatorExecutor.SwitchPriority<UnitCreator>();
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

        if (_creatorExecutor.CurrentPrice <= _deliveredBoxes.Count)
            Execute();
    }

    private void Execute()
    {
        Box[] cost = _deliveredBoxes.Take(_creatorExecutor.CurrentPrice).ToArray();

        _deliveredBoxes = _deliveredBoxes.Except(cost).ToList();
        _creatorExecutor.TryExecute(cost, this, UnitSpawnPosition);
    }
}

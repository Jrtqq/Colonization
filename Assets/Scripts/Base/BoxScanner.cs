using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxScanner
{
    public UnityAction<Box> BoxFound;

    private Vector3 _radius = new(100, 0.5f, 100);
    private Vector3 _center;
    private float _time;

    public BoxScanner(Vector3 center)
    {
        _center = center;

        _time = Config.BoxSpawnCooldown;
    }

    public void Update()
    {
        _time += Time.deltaTime;

        if (_time > Config.BoxSpawnCooldown + Time.deltaTime)
        {
            _time = 0;
            Scan();
        }
    }

    private void Scan()
    {
        Collider[] hit = Physics.OverlapBox(_center, _radius);

        foreach (Collider collider in hit)
        {
            if (collider.TryGetComponent(out Box box) && box.IsMarked == false)
            {
                BoxFound?.Invoke(box);
                break;
            }
        }
    }
}

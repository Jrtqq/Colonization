using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Test : MonoBehaviour
{
    private TestPrefab _prefab;

    private void Awake()
    {
        _prefab = Resources.Load<TestPrefab>("Prefabs/Tests/Test");
        Debug.Log(_prefab);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            _prefab.Init();
    }
}

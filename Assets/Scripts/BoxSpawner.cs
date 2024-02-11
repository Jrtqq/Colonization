using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private Box _prefab;

    private Bounds _spawnRange;
    private float _time = 0;

    private void Awake()
    {
        _spawnRange = GetComponent<Collider>().bounds;
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_time >= Config.BoxSpawnCooldown)
        {
            _time = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        int x = (int)Random.Range(_spawnRange.min.x, _spawnRange.max.x);
        int z = (int)Random.Range(_spawnRange.min.z, _spawnRange.max.z);

        Instantiate(_prefab, new(x, _prefab.transform.position.y, z), Quaternion.identity);
    }
}

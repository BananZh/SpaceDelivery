using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid3DPlacer : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs3D;
    [SerializeField] private Vector3Int _gridSize = new Vector3Int(5, 5, 5);
    [SerializeField] private Vector3 _cellSize = new Vector3(10, 10, 10);
    [SerializeField] private float _minScale = 1f;
    [SerializeField] private float _maxScale = 50f;
    [SerializeField] private int _batchSize = 10;
    [SerializeField] private float _randOffset = 1f;
    [SerializeField] private LayerMask _noSpawnLayerMask;
    [SerializeField] private float _spawnRate = 0.5f;


    void Start()
    {
        StartCoroutine(SpawnGridAsync());
    }
    private List<Vector3> GetAllPositions()
    {
        List<Vector3> positions = new List<Vector3>();
        for (int x = -_gridSize.x / 2; x < _gridSize.x / 2 + _gridSize.x % 2; x++)
        {
            for (int y = -_gridSize.y / 2; y < _gridSize.y / 2 + _gridSize.y % 2; y++)
            {
                for (int z = -_gridSize.z / 2; z < _gridSize.z / 2 + _gridSize.z % 2; z++)
                {
                    if (1 - Random.value < _spawnRate)
                    {
                        Vector3 position = new Vector3(
                            x * _cellSize.x,
                            y * _cellSize.y,
                            z * _cellSize.z
                        ) + transform.position
                        + Random.onUnitSphere * Random.Range(-_randOffset, _randOffset);
                        positions.Add(position);
                    }
                }
            }
        }
        return positions;
    }

    IEnumerator SpawnGridAsync()
    {
        int spawned = 0;
        List<Vector3> positions = GetAllPositions();
        foreach (Vector3 pos in positions)
        {
            SpawnPrefabAtRandomPosition(pos);
            if (++spawned >= _batchSize)
            {
                yield return null;
                print($"Spawned {spawned} items of {positions.Count}");
                spawned = 0;
            }
        }
        print("Spawn over!");
    }
    void SpawnPrefabAtRandomPosition(Vector3 position)
    {
        int randPrefabIndex = Random.Range(0, _prefabs3D.Length);
        float randScale = Random.Range(_minScale, _maxScale);

        GameObject randPrefab = _prefabs3D[randPrefabIndex];
        Bounds bounds = randPrefab.GetComponent<Collider>().bounds;
        bounds.size *= randScale;
        if (DetectHit(bounds, position)) return; // deny spawn if not allowed

        GameObject newInstance = Instantiate(randPrefab, position, Quaternion.identity, transform);
        newInstance.transform.localScale *= randScale;
        Rigidbody newInstanceRB = newInstance.GetComponent<Rigidbody>();
        newInstanceRB.mass *= randScale;
        Asteroid asteroid = newInstance.AddComponent<Asteroid>();
        asteroid.SetHP(newInstanceRB.mass);
        // print(newInstance.name + "Spawned!");.
    }

    bool DetectHit(Bounds bounds, Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, bounds.extents.magnitude, _noSpawnLayerMask);
        foreach (var col in colliders) if (col.transform != transform)
        {
            // print(col.name);
            return true;
        }
        // print("Spawn granted!"); 
        return false;
    }
}

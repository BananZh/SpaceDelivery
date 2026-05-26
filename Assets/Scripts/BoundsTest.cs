using System.Linq;
using UnityEditor;
using UnityEngine;

public class BoundsTest : MonoBehaviour
{
    // [SerializeField] private GameObject prefab;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        print(DetectHit());
    }

    bool DetectHit()
    {
        Bounds bounds = gameObject.GetComponent<Collider>().bounds;
        Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.extents);
        print(colliders.ToArray());
        foreach (var col in colliders)
            if (col.transform != transform) return true;
        return false;
    }
}

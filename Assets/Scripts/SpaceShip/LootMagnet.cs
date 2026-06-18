using UnityEngine;

public class LootMagnet : MonoBehaviour
{
    [SerializeField] private float _magnetDistance = 10f;
    [SerializeField] private float _magnetForce = 1f;
    [SerializeField] private LayerMask _lootLayerMask;
    private Inventory _inventory;
    private GameManager gameManager;
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        _inventory = transform.root.GetComponentInChildren<Inventory>();
        if (!_inventory) print("Inventory was not initialized!");
    }

    void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _magnetDistance, _lootLayerMask);
        foreach (Collider col in colliders)
        {
            if (col.TryGetComponent<Rigidbody>(out var rb))
            {
                Vector3 vector = transform.position - col.transform.position;
                Vector3 direction = vector.normalized;
                float distance = vector.magnitude;
                rb.AddForce(direction * _magnetForce / distance);
                print(col.name);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Loot"))
        {
            Destroy(other.gameObject);
            _inventory.AddItemIntoInventorySlot(other.gameObject, 1);
        }
    }
}

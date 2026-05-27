using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    private InputAction _shootAction;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _gun1Transform;
    [SerializeField] private Transform _gun2Transform;
    [SerializeField] private float _bulletForce = 3f;
    private Rigidbody _shipRB;
    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _bulletSize = 0.3f;

    void Start()
    {
        _shootAction = InputSystem.actions.FindAction("Attack");
        _shipRB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_shootAction.WasPressedThisFrame())
        {
            GameObject bulletInstance = Instantiate(_bulletPrefab, _gun1Transform.position, Quaternion.identity);
            Destroy(bulletInstance, 3f);
            Vector3 direction = transform.TransformDirection(Vector3.forward);
            Rigidbody bulletRB = bulletInstance.GetComponent<Rigidbody>();
            Bullet bullet = bulletInstance.GetComponent<Bullet>();
            bulletInstance.transform.localScale = _bulletSize * Vector3.one;
            bulletRB.linearVelocity = _shipRB.linearVelocity + direction * _bulletForce;
            bullet._damage = _damage;
        }
    }
}

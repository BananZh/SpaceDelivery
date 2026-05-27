using System;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _shootAction = InputSystem.actions.FindAction("Attack");
        _shipRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_shootAction.WasPressedThisFrame())
        {
            GameObject bullet = Instantiate(_bulletPrefab, _gun1Transform.position, Quaternion.identity);
            // Destroy(bullet, 3f);
            Vector3 direction = transform.TransformDirection(Vector3.forward);
            Rigidbody bulletRB = bullet.GetComponentInChildren<Rigidbody>();
            bulletRB.linearVelocity = _shipRB.linearVelocity + direction * _bulletForce;
            print(_shipRB.linearVelocity);
        }
    }
}

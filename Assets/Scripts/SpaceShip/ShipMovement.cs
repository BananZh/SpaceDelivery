using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovement : MonoBehaviour
{
    private InputAction _moveAction;
    private InputAction _brakeAction;
    private InputAction _boostAction;
    private Rigidbody _rb;

    [Header("Speed Parameters")]
    [SerializeField] private float _xForce = 1f;
    [SerializeField] private float _yForce = 1f;
    [SerializeField] private float _zForce = 1f;

    [Header("Modificators")]
    public bool breaking = false;
    public bool boosting = false;
    [SerializeField] private float _breakDamping = 1f;
    [SerializeField] private float _defaultDamping = 0.1f;
    public float boostValue = 3f;

    [Header("Utilities")]
    public Vector3 moveValue;

    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _brakeAction = InputSystem.actions.FindAction("Brakes");
        _boostAction = InputSystem.actions.FindAction("Boost");
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moveValue = _moveAction.ReadValue<Vector3>();
        boosting = _boostAction.ReadValue<float>() != 0;
        breaking = _brakeAction.ReadValue<float>() != 0;

        _rb.AddRelativeForce(moveValue.x * _xForce * Vector3.right, ForceMode.Force);
        _rb.AddRelativeForce(moveValue.y * _yForce * Vector3.up, ForceMode.Force);
        _rb.AddRelativeForce(moveValue.z * _zForce * Vector3.forward * (boosting ? boostValue : 1f), ForceMode.Force);

        if (breaking) _rb.linearDamping = _breakDamping;
        else _rb.linearDamping = _defaultDamping;
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class ShipRotateToCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cameraPoint;

    [Header("Rotation settings")]
    [SerializeField] private float torqueStrength = 15f;
    [SerializeField] private float rotationDamping = 2f;

    private Rigidbody rb;
    private InputAction moveAction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void FixedUpdate()
    {
        Vector3 moveInput = moveAction.ReadValue<Vector3>();

        // Поворачиваемся только при тяге
        // if (moveInput.z == 0f)
        //     return;

        // Целевая ориентация = ориентация камеры
        Quaternion targetRotation = cameraPoint.rotation;
        Quaternion currentRotation = rb.rotation;

        // Разница вращений
        Quaternion deltaRotation = targetRotation * Quaternion.Inverse(currentRotation);

        deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);

        // Приводим угол к диапазону [-180; 180]
        if (angle > 180f)
            angle -= 360f;

        // Крутящий момент
        Vector3 torque =
            axis.normalized *
            angle *
            Mathf.Deg2Rad *
            torqueStrength;

        // Демпфирование угловой скорости
        torque -= rb.angularVelocity * rotationDamping;

        rb.AddTorque(torque, ForceMode.Force);
    }
}

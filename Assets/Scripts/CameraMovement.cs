using UnityEngine;
using UnityEngine.InputSystem;
public class CameraMovement : MonoBehaviour
{
    private InputAction mouseAction;
    [SerializeField] private float mouseSens = 0.1f;
    private GameObject player;
    private InputAction rotateAction;
    [SerializeField] private float rotationSpeed = 1f;

    void Start()
    {
        mouseAction = InputSystem.actions.FindAction("Look");
        player = GameObject.FindGameObjectWithTag("Player");
        rotateAction = InputSystem.actions.FindAction("RotateCam");
    }

    void Update()
    {
        transform.position = player.transform.position;
        Vector2 mouseValue = mouseAction.ReadValue<Vector2>() * mouseSens;
        float rotateValue = rotateAction.ReadValue<float>();

        if (mouseValue != Vector2.zero)
        {
            transform.Rotate(new Vector3(-mouseValue.y, mouseValue.x, 0));
        }

        if (rotateValue != 0)
        {
            transform.Rotate(0, 0, rotationSpeed * rotateValue * Time.deltaTime);
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(Canvas))]

public class MaintainedSizeBillboard : MonoBehaviour
{
    [Header("Настройки размера")]
    [SerializeField] private float baseScale = 0.01f; // Базовый масштаб Canvas
    [SerializeField] private float sizeMultiplier = 1.0f; // Коэффициент размера
    [SerializeField] private float hideDistance = 100.0f; // Дистанция выключения видимости
    private Canvas canvas; // Canvas, которому отключается видимость

    private Transform mainCameraTransform;

    void Start()
    {
        if (Camera.main != null)
        {
            mainCameraTransform = Camera.main.transform;
        }
        canvas = GetComponent<Canvas>();
    }

    void LateUpdate()
    {
        if (mainCameraTransform == null) return;

        // 1. Поворот к камере (Биллбординг)
        transform.rotation = mainCameraTransform.rotation;

        // 2. Расчет статичного размера
        // Вычисляем расстояние по вектору направления камеры (защищает от искажений на краях экрана)
        float distance = Vector3.Dot(transform.root.position - mainCameraTransform.position, mainCameraTransform.forward);

        if (distance > 0)
        {
            float currentScale = distance * baseScale * sizeMultiplier;
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);
        }

        if (distance < hideDistance) canvas.enabled = false;
        else if (!canvas.enabled) canvas.enabled = true;
    }
}
using System.Runtime.InteropServices;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class DistanceToBase : MonoBehaviour
{
    private Transform worldPointTransform;
    private Transform playerTransform;
    private TextMeshProUGUI outputText;
    void Start()
    {
        worldPointTransform = transform.root;
        playerTransform = Camera.main.transform;
        outputText = GetComponent<TextMeshProUGUI>();
    }

    void LateUpdate()
    {
        float distance = Mathf.Round(Vector3.Distance(worldPointTransform.position, playerTransform.position) * 10) / 10;
        outputText.text = $"{distance}m";
    }
}

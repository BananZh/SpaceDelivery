using System;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text _speedText;
    [SerializeField] private Rigidbody _playerRGB;
    void Start()
    {
        // Application.targetFrameRate = 100;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnGUI()
    {
        _speedText.text = Math.Round(_playerRGB.linearVelocity.magnitude, 1).ToString("F1");
    }
}

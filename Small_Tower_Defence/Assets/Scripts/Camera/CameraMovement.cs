using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera Movement")]
    [SerializeField] private float _moveSpeed = 4.0f;
    [SerializeField] private float _panBorderThickness = 10.0f;

    private CameraController _cameraController;

    private void Start()
    {
        _cameraController = GetComponent<CameraController>();
    }

    public void SetMouseMoveInput()
    {
        if (!_cameraController.CanMove) return;

        // store mouse position in runtime
        Vector3 mousePos = Mouse.current.position.ReadValue();

        if (mousePos.y >= Screen.height - _panBorderThickness)
        {
            transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime, Space.World);
        }
        if (mousePos.y <= _panBorderThickness)
        {
            transform.Translate(Vector3.back * _moveSpeed * Time.deltaTime, Space.World);
        }
        if (mousePos.x >= Screen.width - _panBorderThickness)
        {
            transform.Translate(Vector3.right * _moveSpeed * Time.deltaTime, Space.World);
        }
        if (mousePos.x <= _panBorderThickness)
        {
            transform.Translate(Vector3.left * _moveSpeed * Time.deltaTime, Space.World);
        }
    }
}

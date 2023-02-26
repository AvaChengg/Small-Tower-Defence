using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera Movement")]
    [SerializeField] private float _moveSpeed = 4.0f;
    [SerializeField] private float _panBorderThickness = 10.0f;

    private Vector3 _moveInput;
    private CameraController _cameraController;

    private void Start()
    {
        _cameraController = GetComponent<CameraController>();
    }

    public void SetMoveInput(bool isMouseInput)
    {
        if (isMouseInput) return;

        transform.position = Vector3.Lerp(transform.position, _cameraController.MoveTarget, _moveSpeed * Time.deltaTime);
    }

    public void SetMouseMoveInput(bool isMouseInput)
    {
        if (_cameraController.MousePos.y >= Screen.height - _panBorderThickness)
        {
            _cameraController.IsMouseInput = true;
            transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime, Space.World);
        }
        if (_cameraController.MousePos.y <= _panBorderThickness)
        {
            _cameraController.IsMouseInput = true;
            transform.Translate(Vector3.back * _moveSpeed * Time.deltaTime, Space.World);
        }
        if (_cameraController.MousePos.x >= Screen.width - _panBorderThickness)
        {
            _cameraController.IsMouseInput = true;
            transform.Translate(Vector3.right * _moveSpeed * Time.deltaTime, Space.World);
        }
        if (_cameraController.MousePos.x <= _panBorderThickness)
        {
            _cameraController.IsMouseInput = true;
            transform.Translate(Vector3.left * _moveSpeed * Time.deltaTime, Space.World);
        }
    }

}

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

    public void SetMoveInput()
    {
        transform.position = Vector3.Lerp(transform.position, _cameraController.MoveTarget, Time.deltaTime * _moveSpeed);
    }

    public void SetMouseMoveInput()
    {
        if (_cameraController.MousePos.y >= Screen.height - _panBorderThickness)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _moveSpeed, Space.World);
        }
        if (_cameraController.MousePos.y <= _panBorderThickness)
        {
            transform.Translate(Vector3.back * Time.deltaTime * _moveSpeed, Space.World);
        }
        if (_cameraController.MousePos.x >= Screen.width - _panBorderThickness)
        {
            transform.Translate(Vector3.right * Time.deltaTime * _moveSpeed, Space.World);
        }
        if (_cameraController.MousePos.x <= _panBorderThickness)
        {
            transform.Translate(Vector3.left * Time.deltaTime * _moveSpeed, Space.World);
        }
    }

}

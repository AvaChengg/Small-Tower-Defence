using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera Movement")]
    [SerializeField] private float _moveSpeed = 4.0f;

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

}

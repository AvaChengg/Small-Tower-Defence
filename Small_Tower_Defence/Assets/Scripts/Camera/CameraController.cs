using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _panSpeed = 30.0f;
    [SerializeField] private float panBorderThickness = 10.0f;

    private Vector3 _input;
    private Vector3 _localMoveInput;
    private bool _hasMoveInput;

    public void SetMoveInput(Vector3 moveInput)
    {

        //moveInput = Vector3.ClampMagnitude(moveInput, 1f);

        //// set input to 0 if small incoming value
        //_hasMoveInput = moveInput.magnitude > 0.1f;
        //moveInput = _hasMoveInput ? moveInput : Vector3.zero;

        //// remove y component of movement but retain overall magnitude
        //Vector3 flattened = new Vector3(moveInput.x, 0f, moveInput.z);
        //flattened = flattened.normalized * moveInput.magnitude;
        //_input = flattened;

        //// finds movement input as local direction rather than world direction
        //_localMoveInput = transform.InverseTransformDirection(_input);
    }

    public void SetMouseMoveInput()
    {
        if (Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * _panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * _panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * _panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * _panSpeed * Time.deltaTime, Space.World);
        }
    }
    private void Update()
    {
        SetMouseMoveInput();
    }
}

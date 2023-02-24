using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;

    private Vector3 _moveInput;
    private CameraController _cameraMovement;
    private Targetable _targetable;
    private bool _isSelect;

    private void Awake()
    {
        _cameraMovement = GetComponent<CameraController>();
        _targetable = GetComponent<Targetable>();
    }

    // receive move input from PlayerInput component (Vector2)
    public void OnMove(InputValue value)
    {
        // read Vector2 data from InputValue
        Vector2 input = value.Get<Vector2>();
        _moveInput = new Vector3(input.x, 0, input.y);

        Debug.DrawRay(transform.position, _moveInput, Color.magenta, 0.1f);
    }

    public void OnSelect()
    {
        _isSelect = true;
    }

    private void Update()
    {
        if (_cameraMovement == null) return;

        // send move input to CameraController component
        //_cameraMovement.SetMoveInput(_moveInput);
        //_cameraMovement.SetMouseMoveInput();
        // find mouse ray
        //Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //// cast mouseRay against groundMask
        //if (Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Infinity, _groundMask))
        //{
        //    // place building
        //    Vector3 firePoint = hit.point - mouseRay.direction;
        //    if (_isSelect)
        //    {

        //    }
        //}

    }

}

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Camera Setting")]
    [SerializeField] private float _lookOffset = 1.0f;
    [SerializeField] private float _cameraAngle = 45.0f;
    [SerializeField] private float _defaultZoom = 5.0f;
    [SerializeField] private float _targetSpeed = 8.0f;
    [SerializeField] private LayerMask _groundMask;

    private Vector3 _moveInput;
    private Vector3 _cameraPosition;
    private CinemachineVirtualCamera _levelCamera;
    private CameraMovement _cameraMovement;
    private Targetable _targetable;
    private bool _isSelect;

    [HideInInspector] public bool IsMouseInput;
    [HideInInspector] public Vector3 MoveTarget;
    [HideInInspector] public Vector3 MousePos;

    private void Awake()
    {
        _cameraMovement = GetComponent<CameraMovement>();
        _targetable = GetComponent<Targetable>();
    }
    private void Start()
    {
        _levelCamera = GetComponentInChildren<CinemachineVirtualCamera>();

        // set the rotation of the camera based on the CameraAngle property
        _levelCamera.transform.rotation = Quaternion.AngleAxis(_cameraAngle, Vector3.right);

        // set the position of the camera based on the look offset, angle and default zoom properties
        _cameraPosition = (Vector3.up * _lookOffset) + (Quaternion.AngleAxis(_cameraAngle, Vector3.right) * Vector3.back) * _defaultZoom;

        _levelCamera.transform.position = _cameraPosition;
    }

    // receive move input from PlayerInput component (Vector2)
    public void OnMove(InputAction.CallbackContext context)
    {
        // check if it's input by keyboard or mouse
        IsMouseInput = false;

        // read Vector2 data from InputValue
        Vector2 value = context.ReadValue<Vector2>();
        _moveInput = new Vector3(value.x, 0, value.y);
    }

    public void OnSelect()
    {
        _isSelect = true;
    }

    private void FixedUpdate()
    {
        MoveTarget += (transform.forward * _moveInput.z + transform.right * _moveInput.x) * Time.fixedDeltaTime * _targetSpeed;
    }

    private void Update()
    {
        if (_cameraMovement == null) return;

        // send move input to CameraMovement component
        _cameraMovement.SetMoveInput(IsMouseInput);
        //_cameraMovement.SetMouseMoveInput(IsMouseInput);

        // find mouse ray
        MousePos = Mouse.current.position.ReadValue();
        Ray mouseRay = Camera.main.ScreenPointToRay(MousePos);

        //// cast mouseRay against groundMask
        if (Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Infinity, _groundMask))
        {
            // place building
            Vector3 firePoint = hit.point - mouseRay.direction;
            Debug.DrawLine(_levelCamera.transform.position, hit.transform.position, Color.red);

            if (hit.transform.TryGetComponent(out Targetable possibleTarget) && possibleTarget.Team == 3)
            {
                hit.transform.GetComponent<Renderer>().material.SetColor("_Occlusion", Color.green);
            }

            //Debug.DrawRay(_levelCamera.transform.position, mouseRay.direction, Color.red, 0.1f);
            if (_isSelect)
            {

            }
        }

    }

}

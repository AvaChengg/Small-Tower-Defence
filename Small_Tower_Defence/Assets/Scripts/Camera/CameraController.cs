using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Camera Setting")]
    [SerializeField] private float _lookOffset = 1.0f;
    [SerializeField] private float _cameraAngle = 45.0f;
    [SerializeField] private float _defaultZoom = 5.0f;
    [SerializeField] private LayerMask _groundMask;

    private Vector3 _cameraPosition;
    private PlayerController _playerController;
    private CameraMovement _cameraMovement;
    private CinemachineVirtualCamera _levelCamera;

    [HideInInspector] public bool CanMove;
    [HideInInspector] public LayerMask GroundMask => _groundMask;
    [HideInInspector] public Vector3 MoveTarget;
    [HideInInspector] public Vector3 MousePos;

    private void Awake()
    {
        _cameraMovement = GetComponent<CameraMovement>();
        _playerController = GetComponent<PlayerController>();
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

    // stop moving camera by mouse
    public void OnStopMovingCamera(InputAction.CallbackContext context)
    {
        CanMove = !CanMove;
    }

    // place the building
    public void OnPlace(InputAction.CallbackContext context)
    {
        if(!_playerController.IsSpot) return;
        _playerController.PlaceBuliding();
    }

    private void Update()
    {
        if (_cameraMovement == null) return;

        // send move input to CameraMovement component
        _cameraMovement.SetMouseMoveInput();

    }
}
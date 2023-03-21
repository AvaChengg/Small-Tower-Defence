using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Camera Setting")]
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
        _cameraPosition = _levelCamera.transform.position;
    }

    // return to original position
    public void OnReturnCamera(InputAction.CallbackContext context)
    {
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
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Placement Spots Setting")]
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _highlightColor;

    [Header("Buildings")]
    [SerializeField] private GameObject[] _buildings;

    private int _buildingNum;
    private bool _isSelect;
    private Vector3 _placePoint;
    private RaycastHit _hit;

    private GameObject _placementSpot;
    private CameraController _cameraController;
    private CinemachineVirtualCamera _levelCamera;

    public int BuildingNum { get => _buildingNum; set => _buildingNum = value; }

    [HideInInspector] public bool IsSpot;

    private void Start()
    {
        _levelCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        _cameraController = GetComponent<CameraController>();
    }

    void Update()
    {
        // find mouse ray
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);

        // cast mouseRay against groundMask
        if (Physics.Raycast(mouseRay, out _hit, Mathf.Infinity, _cameraController.GroundMask))
        {
            if (!_isSelect) return;

            // place building
            _placePoint = _hit.point - mouseRay.direction;
            Debug.DrawLine(_levelCamera.transform.position, _hit.transform.position, Color.red);

            // find possible placement spots
            if (_hit.transform.TryGetComponent(out Targetable possibleTarget) &&
                possibleTarget.Team == 3 &&
                possibleTarget.IsTargetable)
            {
                _placementSpot = _hit.transform.gameObject;
                _hit.transform.GetComponent<Renderer>().material.color = _highlightColor;
                IsSpot = true;
            }
        }
        else
        {
            if (_placementSpot == null) return;
            IsSpot = false;
            _placementSpot.GetComponent<Renderer>().material.color = _defaultColor;
        }
    }

    public void SelectBuliding()
    {
        _isSelect = true;
    }

    public void PlaceBuliding()
    {
        if (_buildings == null) return;

        switch (_buildingNum)
        {
            case 0:
                Instantiate(_buildings[0], _hit.transform.position, _hit.transform.rotation);
                break;
            case 1:
                Instantiate(_buildings[1], _hit.transform.position, _hit.transform.rotation);
                break;
        }
        _placementSpot.SetActive(false);
        IsSpot = false;
        _isSelect = false;
    }
}

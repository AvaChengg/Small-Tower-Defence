using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    [SerializeField] private float _attackDistance = 5.0f;
    [SerializeField] private float _viewDistance = 10.0f;
    [SerializeField] private float _viewHalfAngle = 70.0f;
    [SerializeField] private LayerMask _occlusionMask;
    [SerializeField] private LayerMask _targetMask;

    private Targetable _myTargetable;
    private Targetable _target;

    // useful properties for AI decision making
    public bool IsTargetValid => _target != null && _target.IsTargetable;
    public float TargetDistance => Vector3.Distance(_target.Position, transform.position);
    //public Vector3 TargetDirection => (_target.Position - transform.position).normalized;
    public bool IsTargetVisible => HelperFunctions.TestVisibility(_myTargetable.AimPosition.position, _target.AimPosition.position + Vector3.up, _viewDistance, transform.forward, _viewHalfAngle, _occlusionMask);

    private void Awake()
    {
        _myTargetable= GetComponent<Targetable>();
    }

    private void Update()
    {
        // find targe if lacking one
        if(!IsTargetValid) TryFindTarget();

        // chase target if valid
        if(IsTargetValid)
        {
            // chase target if out of range or visibility
            if (TargetDistance > _attackDistance || !IsTargetVisible)
            {
                Debug.Log("Shoot");
            }
            else
            {
                Debug.Log("Stop!");
            }
        }
    }

    // attempt to find valid target within view
    private void TryFindTarget()
    {
        // find all colliders within radius, on specificed mask
        Collider[] hits = Physics.OverlapSphere(transform.position, _viewDistance, _targetMask);

        // iterate through all *possible* targets (may include allies or ourself)
        foreach (Collider hit in hits)
        {
            Debug.DrawLine(_myTargetable.AimPosition.position, hit.transform.position, Color.white);

            // check for valid target, using TryGetComponent to get a possible Targetable component and assign it in one line
            if(hit.TryGetComponent(out Targetable possibleTarget) &&
                possibleTarget.IsTargetable &&
                possibleTarget.Team != _myTargetable.Team &&
                HelperFunctions.TestVisibility(_myTargetable.AimPosition.position, possibleTarget.AimPosition.position, _viewDistance, transform.forward, _viewHalfAngle, _occlusionMask))
            {
                // if all tests succeed, we have found a target
                _target = possibleTarget;
                break; // break exits out of loop because we have found a target
            }
        }
    }
}

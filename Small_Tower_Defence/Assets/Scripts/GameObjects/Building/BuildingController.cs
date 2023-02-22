using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel.Configuration;
using UnityEngine;

public class BuildingController : MovementState
{
    [Header("Attacking")]
    [SerializeField] private float _attackDistance = 5.0f;
    [SerializeField] private float _viewDistance = 10.0f;
    [SerializeField] private float _viewHalfAngle = 70.0f;
    [SerializeField] private LayerMask _occlusionMask;
    [SerializeField] private LayerMask _targetMask;

    private Targetable _myTargetable;
    private Targetable _target;
    private Shooter _shooter;

    private IEnumerator _buildingCurrentState;

    // useful properties for AI decision making
    public bool IsTargetValid => _target != null && _target.IsTargetable;
    public float TargetDistance => Vector3.Distance(_target.Position, transform.position);
    //public Vector3 TargetDirection => (_target.Position - transform.position).normalized;
    public bool IsTargetVisible => HelperFunctions.TestVisibility(_myTargetable.AimPosition.position, _target.AimPosition.position + Vector3.up, _viewDistance, transform.forward, _viewHalfAngle, _occlusionMask);

    private void Awake()
    {
        _myTargetable = GetComponent<Targetable>();
        _shooter = GetComponentInChildren<Shooter>();
    }

    private void Start()
    {
        ChangeState(IdleState(), _buildingCurrentState);
    }

    protected override void ChangeState(IEnumerator newState, IEnumerator currentState)
    {
        base.ChangeState(newState, currentState);
    }

    private IEnumerator IdleState()
    {
        // loop waiting for a target to appear
        while (true)
        {
            TryFindTarget();
            if (IsTargetValid) ChangeState(AttackState(), _buildingCurrentState);

            // pause loop and wait for next frame to execute
            yield return null;
        }
    }

    private IEnumerator AttackState()
    {
        // only attack valid targets
        while (IsTargetValid)
        {
            // attack if within range and LoS
            if (TargetDistance < _attackDistance && IsTargetVisible)
            {
                // shoot monsters
                _shooter.TryFire(_target.AimPosition.position, _myTargetable.Team, gameObject);
            }

            // wait for next frame
            yield return null;
        }

        //// fall back to idle if target is invalid
        //ChangeState(IdleState(), _buildingCurrentState);

    }

    // attempt to find valid target within view
    private void TryFindTarget()
    {
        // find all colliders within radius, on specificed mask
        Collider[] _hits = Physics.OverlapSphere(transform.position, _viewDistance, _targetMask);

        // iterate through all *possible* targets (may include allies or ourself)
        foreach (Collider hit in _hits)
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

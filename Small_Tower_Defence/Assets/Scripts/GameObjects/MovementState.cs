using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : MonoBehaviour
{
    [Header("Attacking")]
    [SerializeField] protected float _attackDistance = 5.0f;
    [SerializeField] protected float _viewDistance = 10.0f;
    [SerializeField] protected float _viewHalfAngle = 70.0f;
    [SerializeField] protected LayerMask _occlusionMask;
    [SerializeField] protected LayerMask _targetMask;

    // protected
    protected Targetable _myTargetable;
    protected Targetable _target;
    protected Shooter _shooter;
    protected string _currentStateName;

    // public
    // useful properties for AI decision making
    public bool IsTargetValid => _target != null && _target.IsTargetable;
    public float TargetDistance => Vector3.Distance(_target.Position, transform.position);
    //public Vector3 TargetDirection => (_target.Position - transform.position).normalized;
    public bool IsTargetVisible => HelperFunctions.TestVisibility(_myTargetable.AimPosition.position, _target.AimPosition.position + Vector3.up, _viewDistance, transform.forward, _viewHalfAngle, _occlusionMask);

    protected void Awake()
    {
        _myTargetable = GetComponent<Targetable>();
        _shooter = GetComponentInChildren<Shooter>();
    }

    protected virtual void ChangeState(IEnumerator newState, IEnumerator currentState)
    {
        // stop current state if it exits
        if (currentState != null) StopCoroutine(currentState);

        // assign new current state and start it
        currentState = newState;

        StartCoroutine(currentState);
    }

    // attempt to find valid target within view
    protected void TryFindTarget()
    {
        // find all colliders within radius, on specificed mask
        Collider[] _hits = Physics.OverlapSphere(transform.position, _viewDistance, _targetMask);

        // iterate through all *possible* targets (may include allies or ourself)
        foreach (Collider hit in _hits)
        {
            Debug.DrawLine(_myTargetable.AimPosition.position, hit.transform.position, Color.white);

            // check for valid target, using TryGetComponent to get a possible Targetable component and assign it in one line
            if (hit.TryGetComponent(out Targetable possibleTarget) &&
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

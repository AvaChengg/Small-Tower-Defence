using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel.Configuration;
using UnityEngine;
using UnityEngine.VFX;

public class BuildingController : MovementState
{
    private IEnumerator _buildingCurrentState;

    [Header("Reference")]
    [SerializeField] private GameObject _towerPlacementSpot;
    [SerializeField] private AudioSource _attackSFX;

    [Header("VFX")]
    [SerializeField] private VisualEffect _arrows;

    public bool IsUpgraded;

    private void Start()
    {
        ChangeState(IdleState(), _buildingCurrentState);
    }

    protected override void ChangeState(IEnumerator newState, IEnumerator currentState)
    {
        _buildingCurrentState = newState;
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
                // set SFX
                _attackSFX.Play();

                // shoot monsters
                _shooter.TryFire(_target.AimPosition.position, _myTargetable.Team, gameObject);

                // set VFX
                _arrows.transform.LookAt(_target.AimPosition);
                _arrows.Play();

            }

            // wait for next frame
            yield return null;
        }

        // fall back to idle if target is invalid
        ChangeState(IdleState(), _buildingCurrentState);
    }

    public void Destroy()
    {
        Instantiate(_towerPlacementSpot, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

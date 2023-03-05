using System.Collections;
using System.EnterpriseServices;
using System.Windows.Forms;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class EnemyController : MovementState
{
    [Header("Patrolling")]
    [SerializeField] private float _patrolPointReachedDistance = 1.0f;
    [SerializeField] private float _patrolSpeed = 0.5f;
    private PatrolPoint[] _patrolPoints;

    // get patrol points data from Encounter.cs and then store the date to _patrolPoints
    public PatrolPoint[] PatrolPoints { get { return _patrolPoints; } set { _patrolPoints = value; } }
    private int _pathIndex = 0;

    // private
    private CharacterMovement _charcterMovement;
    private IEnumerator _monsterCurrentState;

    // Events
    public UnityEvent OnKilled;

    private void Start()
    {
        _charcterMovement = GetComponent<CharacterMovement>();

        // start in patrol state
        ChangeState(PatrolState(), _monsterCurrentState);
    }

    protected override void ChangeState(IEnumerator newState, IEnumerator currentState)
    {
        // reset move speed mulitplier
        _charcterMovement.SpeedMultiplier = 1.0f;
        _monsterCurrentState = newState;

        base.ChangeState(newState, currentState);

    }
    // patrol the points by the order
    private IEnumerator PatrolState()
    {
        // slow move speed during patrol
        _charcterMovement.SpeedMultiplier = _patrolSpeed;

        while(true)
        {
            TryFindTarget();
            if (IsTargetValid) AttackBuilding();

            // find new patrol point if existing reached
            float patrolDistance = Vector3.Distance(transform.position, _patrolPoints[_pathIndex].transform.position);
            if (patrolDistance < _patrolPointReachedDistance && _pathIndex != _patrolPoints.Length) _pathIndex ++;

            // check if moster die or reach the final point
            if (_pathIndex == _patrolPoints.Length)
            {
                StopState();
                yield return false;
            }

            // move to patrol point
            _charcterMovement.MoveTo(_patrolPoints[_pathIndex].transform.position);

            // wait for next frame
            yield return null;
        }
    }

    // stop execution of current state
    public void StopState()
    {
        if (_monsterCurrentState != null) StopCoroutine(_monsterCurrentState);

        _charcterMovement.Stop();
    }

    private void AttackBuilding()
    {
        if (TargetDistance < _attackDistance && IsTargetVisible)
        {
            // shoot monsters
            _shooter.TryFire(_target.AimPosition.position, _myTargetable.Team, gameObject);
        }
    }
}

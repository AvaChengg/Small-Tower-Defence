using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    [Header("Patrolling")]
    [SerializeField] private float _patrolPointReachedDistance = 1.0f;
    [SerializeField] private float _patrolSpeed = 0.5f;
    [SerializeField] private PatrolPoint[] _patrolPoints;

    // get patrol points data from Encounter.cs and then store the date to _patrolPoints
    public PatrolPoint[] PatrolPoints { get { return _patrolPoints; } set { _patrolPoints = value; } }
    [SerializeField] private int _pathIndex = 0;

    private IEnumerator _currentState;
    private CharacterMovement _charcterMovement;
    private string _currentStateName;

    // Events
    public UnityEvent OnKilled;

    private void Awake()
    {
        //_target = GameObject.Find("Building");
        _charcterMovement = GetComponent<CharacterMovement>();
    }

    private void Start()
    {
        // start in patrol state
        ChangeState(PatrolState());
    }

    private void ChangeState(IEnumerator newState)
    {
        // stop current state if it exits
        if (_currentState != null) StopCoroutine(_currentState);

        // reset move speed mulitplier
        _charcterMovement.SpeedMultiplier = 1.0f;

        // assign new current state and start it
        _currentState = newState;
        _currentStateName = newState.ToString();

        StartCoroutine(_currentState);
    }

    // stop execution of current state
    public void StopState()
    {
        if(_currentState != null) StopCoroutine(_currentState);
        _charcterMovement.Stop();
    }

    // patrol the points by the order
    private IEnumerator PatrolState()
    {
        // slow move speed during patrol
        _charcterMovement.SpeedMultiplier = _patrolSpeed;

        while(true)
        {
            // find new patrol point if existing reached
            float patrolDistance = Vector3.Distance(transform.position, _patrolPoints[_pathIndex].transform.position);
            if (patrolDistance < _patrolPointReachedDistance) _pathIndex++;

            // move to patrol point
            _charcterMovement.MoveTo(_patrolPoints[_pathIndex].transform.position);

            // check if moster die or reach the final point
            //if (_pathIndex + 1 == _patrolPoints.Length) _charcterMovement.Stop();

            // wait for next frame
            yield return null;
        }

    }
}

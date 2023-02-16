using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    [Header("Attacking")]
    [SerializeField] private float _attackDistance = 5.0f;

    [Header("Patrolling")]
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _patrolPointReachedDistance = 1.0f;
    [SerializeField] private float _patrolSpeed = 0.5f;
    //[SerializeField]protected List<Transform> _patrolPointsAI;
   // public List<Transform> PatrolPointsAI { get { return _patrolPointsAI; } set { _patrolPointsAI = value; } }
    private int _pathIndex = 0;

    private IEnumerator _currentState;
    private CharacterMovement _charcterMovement;
    private string _currentStateName;
    private GameObject _target;

    // Events
    public UnityEvent OnKilled;

    private void Awake()
    {
        //_target = GameObject.Find("Building");
        _charcterMovement = GetComponent<CharacterMovement>();
        //_spawner = GetComponent<Spawner>(); 
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
        Debug.Log("ChangeState");
        StartCoroutine(_currentState);
    }

    // stop execution of current state
    public void StopState()
    {
        if(_currentState != null) StopCoroutine(_currentState);
        _charcterMovement.Stop();
    }

    // sit waiting for a target to appear
    //private IEnumerator IdleState()
    //{
    //    // loop infinitely until a target is found
    //    while(true)
    //    {
    //        TryFindTarget();
    //        if (IsTargetValid) ChangeState(ChaseState());

    //        // pause loop and wait for next frame to execute
    //        yield return null;
    //    }
    //}

    // patrol the points by the order
    private IEnumerator PatrolState()
    {
        // slow move speed during patrol
        _charcterMovement.SpeedMultiplier = _patrolSpeed;

        // find new patrol point if existing reached
        float patrolDistance = Vector3.Distance(transform.position, _patrolPoints[_pathIndex].position);
        if (patrolDistance < _patrolPointReachedDistance) _pathIndex++;

        Debug.Log("PatrolState");

        // move to patrol point
        _charcterMovement.MoveTo(_patrolPoints[_pathIndex].position);

        // wait for next frame
        yield return null;
    }

    // chase after target, enter attack when in range
    //private IEnumerator ChaseState()
    //{
        
    //}

    private void Update()
    {
        if(_target != null)
        {
            Debug.DrawLine(transform.position, _target.transform.position, Color.red);
            _charcterMovement.MoveTo(_target.transform.position);
        }
    }
}

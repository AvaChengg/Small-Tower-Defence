using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _attackDistance = 5.0f;

    [SerializeField] private GameObject _target;
    private CharacterMovement _charcterMovement;

    private IEnumerator _currentState;
    private string _currentStateName;

    private void Awake()
    {
        _target = GameObject.Find("Building");
        _charcterMovement = GetComponent<CharacterMovement>();
    }

    private void ChangeState(IEnumerator newState)
    {
        // stop current state if it exits
        if (_currentState != null) StopCoroutine(_currentState);

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

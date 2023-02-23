using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : MonoBehaviour
{
    protected string _currentStateName;

    protected virtual void ChangeState(IEnumerator newState, IEnumerator currentState)
    {
        // stop current state if it exits
        if (currentState != null) StopCoroutine(currentState);

        // assign new current state and start it
        currentState = newState;

        StartCoroutine(currentState);
    }
}

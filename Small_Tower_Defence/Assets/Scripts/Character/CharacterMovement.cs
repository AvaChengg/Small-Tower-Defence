using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    private float _moveSpeed = 5.0f;
    private float _acceleration = 10.0f;
    private float _turnSpeed = 10.0f;

    private Vector3 _moveInput;
    private Vector3 _lookDirection;
    private Rigidbody _rigidbody;
    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        // configure rigidbody
        _rigidbody= GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    
        // configure NavMeshAgent (turn off movement/ rotation - we'll handle that ourselves)
        _navMeshAgent= GetComponent<NavMeshAgent>();
        _navMeshAgent.updatePosition = false;
        _navMeshAgent.updateRotation= false;

        // set dafault look direction
        _lookDirection = transform.forward;
    }

    public void MoveTo(Vector3 destination, float stoppingDistance = 0.5f)
    {
        float distance = Vector3.Distance(transform.position, destination);
        if (distance < stoppingDistance) Stop();
        else _navMeshAgent.SetDestination(destination);
    }

    public void Stop()
    {
        _navMeshAgent.ResetPath();
        SetMoveInput(Vector3.zero);
    }

    private void SetMoveInput(Vector3 moveInput)
    {
        _moveInput = moveInput;
    }
    private void SetLookDiretion(Vector3 lookDirection)
    {
        lookDirection.y = 0f;
        lookDirection.Normalize();
        _lookDirection = lookDirection;
    }

    private void FixedUpdate()
    {
        if(_navMeshAgent.hasPath) 
        {
            Vector3 next = _navMeshAgent.path.corners[1];
            Vector3 direction = (next - transform.position).normalized;

            SetMoveInput(direction);
            SetLookDiretion(direction);
        }

        // calculate velocity differential
        Vector3 currentVelocity = _rigidbody.velocity;
        Vector3 targetVelocity = _moveInput * _moveSpeed;
        Vector3 velocityDifferentail = targetVelocity - currentVelocity;
        velocityDifferentail.y = 0f;

        // calculate move force
        Vector3 moveForce = velocityDifferentail * _acceleration;

        // apply acceleration
        _rigidbody.AddForce(moveForce);

        // turn lookDirection into a rotation
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(_lookDirection);

        // interpolate from current to target rotation
        Quaternion rotation = Quaternion.Slerp(currentRotation, targetRotation, _turnSpeed * Time.fixedDeltaTime);

        // move to new rotation
        _rigidbody.MoveRotation(rotation);
    }
}

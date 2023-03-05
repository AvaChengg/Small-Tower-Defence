using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownVolume : MonoBehaviour
{
    [SerializeField] private float _slowDownSpeed = 2f;

    private float _moveSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out CharacterMovement enemy)) return;

        _moveSpeed = enemy.MoveSpeed;

        enemy.MoveSpeed = _slowDownSpeed;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out CharacterMovement enemy)) return;

        enemy.MoveSpeed = _moveSpeed;
    }
}

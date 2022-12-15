using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _attackDistance = 5.0f;

    [SerializeField] private GameObject _target;
    private CharacterMovement _charcterMovement;

    private void Awake()
    {
        _target = GameObject.Find("Building");
        _charcterMovement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        if(_target != null)
        {
            Debug.DrawLine(transform.position, _target.transform.position, Color.red);
            _charcterMovement.MoveTo(_target.transform.position);
        }
    }
}

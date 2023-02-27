using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    [SerializeField] private int _team = 1;             // building = 0, monsters = 1, camera = 2, spot = 3
    [SerializeField] private Transform _aimPosition;
    [SerializeField] private bool _isTargetable = true;

    // public get (no set)
    public int Team { get => _team; }
    public Transform AimPosition { get => _aimPosition; }
    public Vector3 Position => transform.position;

    // public get AND set
    public bool IsTargetable { get => _isTargetable; set => _isTargetable = value; }
}

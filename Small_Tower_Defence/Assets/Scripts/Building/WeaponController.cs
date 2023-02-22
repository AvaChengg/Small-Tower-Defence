using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] protected float _damage = 5.0f;
    [SerializeField] protected float _range = 20.0f;
    [SerializeField] protected float _roundsPerMinute = 300.0f;   // the fire period
    [SerializeField] protected LayerMask _hitMask;

    protected float _lastFireTime;

    // attempt to fire at specified position, only damaging enemies
    public virtual void TryFire(Vector3 aimPosition, int myTeam, GameObject instigator)
    {
        // calculate the fire period
        float period = 1f / _roundsPerMinute * 60.0f;

        // test if can fire (stop if not enough time elapsed)
        if (Time.time < _lastFireTime * period) return;
        _lastFireTime = Time.time;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Shooter : WeaponController
{
    [SerializeField] private float _inaccuracy = 5.0f;

    public override void TryFire(Vector3 aimPosition, int myTeam, GameObject instigator)
    {
        base.TryFire(aimPosition, myTeam, instigator);

        // find aim start and direction
        Vector3 start = transform.position;
        Vector3 aimDirection = (aimPosition - start).normalized;

        // add inaccuracy
        Quaternion randomInaccuracy = Quaternion.Euler(0f, Random.Range(-_inaccuracy, _inaccuracy), 0f);
        aimDirection = randomInaccuracy * aimDirection; // (quaterion * Vector3 returns a Vector3, rotated by the quaternion)

        // draw shot cangle
        Debug.DrawRay(start, aimDirection * _range, Color.yellow, 0.25f);

        // raycast in aim direction
        if(Physics.Raycast(start, aimDirection, out RaycastHit hit, _range, _hitMask))
        {
            // if inside this check, something was hit
            
            // try and get target, check team (for enemy), then try to damage health
            if (hit.collider.TryGetComponent(out Targetable target) &&
                target.Team != myTeam &&
                hit.collider.TryGetComponent(out Health targetHealth))
            {
                // create damage info
                DamageInfo info = new DamageInfo();
                info.Amount = _damage;
                info.Instigator = instigator;

                // damage enemy target
                targetHealth.Damage(info);
                Debug.DrawLine(start, hit.point, Color.red, 0.25f);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _current = 100.0f;
    [SerializeField] private float _max = 100.0f;

    //expose useful properties
    public float CurrentHealth => _current;
    public bool IsAlive => _current > 0f;
    public float Percentage => _current / _max;

    public UnityEvent<DamageInfo> OnDamaged;
    public UnityEvent<DamageInfo> OnDeath;

    public void Damage(DamageInfo damageInfo)
    {
        // ensure target is alive
        if (!IsAlive) return;

        // validate incoming damage
        if(damageInfo.Amount < 0f)
        {
            Debug.LogWarning("Don't heal with negative damage!");
            return;
        }

        // modify current health
        _current = Mathf.Clamp(_current - damageInfo.Amount, 0f, _max);

        // invoke OnDamaged event
        OnDamaged?.Invoke(damageInfo);

        // handle death
        if(!IsAlive)
        {
            gameObject.layer = LayerMask.NameToLayer("Corpse");
            gameObject.tag = "Corpse";

            // invoke OnDeath event
            OnDeath?.Invoke(damageInfo);
        }
    }

    // test damage functionality using Context Menu
    [ContextMenu("Test Damage 20%")]
    public void TestDamage()
    {
        // create damage info
        DamageInfo info = new DamageInfo
        {
            Amount = _max * 0.2f,
            Instigator = gameObject,
            Victim = gameObject,
        };

        // cause damage
        Damage(info);
    }
}

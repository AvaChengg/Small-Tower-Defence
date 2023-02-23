using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] private WavesEncounter _wavesEncounter;

    private void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent(out EnemyController enemy) && other.tag != "Corpse") return;

        Health health = enemy.GetComponent<Health>(); 

        if (health.CurrentHealth <= 0)
        {
            _wavesEncounter.RemoveEnemy(enemy);
            enemy.gameObject.SetActive(false);
        }
    }
}

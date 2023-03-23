using UnityEngine;
using UnityEngine.Events;

public class KillZone : MonoBehaviour
{
    [SerializeField] private WavesEncounter _wavesEncounter;
    private int _score = 0;

    public UnityEvent<string> OnUpdateObjective;

    private void Start()
    {
        OnUpdateObjective.Invoke("" + _score);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent(out EnemyController enemy) && other.tag != "Corpse") return;

        Health health = enemy.GetComponent<Health>(); 

        if (health.CurrentHealth <= 0)
        {
            Destroy(enemy.gameObject);
            _wavesEncounter.RemoveEnemy(enemy);

            // add money to the player
            int reward = _wavesEncounter.Coin;
            _score += reward;
            OnUpdateObjective.Invoke("" + _score);
        }
    }
}

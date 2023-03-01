using UnityEngine;
using UnityEngine.Events;

public class KillZone : MonoBehaviour
{
    [SerializeField] private WavesEncounter _wavesEncounter;
    private int _score = 0;

    public UnityEvent<string> OnUpdateObjective;

    private void Start()
    {
        OnUpdateObjective.Invoke("Money: " + _score);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent(out EnemyController enemy) && other.tag != "Corpse") return;

        Health health = enemy.GetComponent<Health>(); 

        if (health.CurrentHealth <= 0)
        {
            _wavesEncounter.RemoveEnemy(enemy);
            enemy.gameObject.SetActive(false);

            // add money to the player
            int reward = _wavesEncounter.Reward;
            _score += reward;
            OnUpdateObjective.Invoke("Money: " + _score);
        }
    }
}

using UnityEngine;
using UnityEngine.Events;

public class KillZone : MonoBehaviour, IInteractable
{
    [SerializeField] private WavesEncounter _wavesEncounter;

    [SerializeField] private int _defaultCoin = 20;

    public UnityEvent<string> OnUpdateObjective;

    public int Money { get => _defaultCoin; set => _defaultCoin = value; }

    private void Start()
    {
        OnUpdateObjective.Invoke("" + _defaultCoin);
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
            _defaultCoin += reward;
            OnUpdateObjective.Invoke("" + _defaultCoin);
        }
    }
}

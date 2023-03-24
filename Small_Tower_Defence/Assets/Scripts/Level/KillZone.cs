using UnityEngine;
using UnityEngine.Events;

public class KillZone : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private WavesEncounter _wavesEncounter;

    [Header("Money Setting")]
    public int DefaultMoney = 20;

    public UnityEvent<string> OnUpdateObjective;

    private void Start()
    {
        OnUpdateObjective.Invoke("" + DefaultMoney);
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
            DefaultMoney += reward;
            OnUpdateObjective.Invoke("" + DefaultMoney);
        }
    }
}

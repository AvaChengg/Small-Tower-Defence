using UnityEngine;
using UnityEngine.Events;

public class TriggerVolume : MonoBehaviour
{
    [SerializeField] private int _lives = 30;                    // player lives
    [SerializeField] private WavesEncounter _wavesEncounter;

    public bool IsGameOver;
    public UnityEvent<GameObject> OnEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out EnemyController enemy)) return;

        // deactivated and remove the monster in list
        _wavesEncounter.RemoveEnemy(enemy);
        enemy.gameObject.SetActive(false);

        CheckLives();

        // kill monsters to start next level
        OnEnter.Invoke(other.gameObject);
    }

    private void CheckLives()
    {
        // remove enemy from the list
        if (_lives > 0) _lives--;

        // check if it's game over
        if (_lives == 0)
        {
            IsGameOver = true;

            // show game over UI
        }
    }
}

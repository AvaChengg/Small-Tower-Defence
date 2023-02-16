using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class Encounter : LevelController
{
    private List<EnemyController> _currentEnemies = new List<EnemyController>();    // store enemies that spawned
    public int CurrentEnemyCounter => _currentEnemies.Count;

    private Spawner _spawner;

    public UnityEvent OnEncounterStarted;
    public UnityEvent OnEncounterFinished;
    public UnityEvent OnUpdateObjected;
    private void Awake()
    {
        _spawner= GetComponent<Spawner>();
    }

    // encounter starts here
    public virtual void StartEncounter()
    {
        OnEncounterStarted?.Invoke();
    }

    protected void SpawnEnemy(EnemyController[] prefabs)
    {
        // check if the game is over or not
        if (_isGameOver == true) return;

        EnemyController spawned = Instantiate(prefabs[_levelCounter], transform.position, transform.rotation) as EnemyController;
        //spawned.PatrolPointsAI = _spawner.PatrolPoints;

        // add to list for tracking and listen for death
        _currentEnemies.Add(spawned);
    }

    // callback from enemies reaching the ending point
    protected void KillEnemy(EnemyController enemy)
    {
        // remove enemy from the list
        if(_currentEnemies.Contains(enemy))
        {
            _currentEnemies.Remove(enemy);
            if (_lives != 0) _lives--;
        }

        // check if it's game over
        if(_lives == 0)
        {
            _isGameOver = true;

            // show game over UI
        }
    }
}

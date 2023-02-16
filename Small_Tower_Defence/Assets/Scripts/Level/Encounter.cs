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

    [SerializeField] private Transform _patrolPoint;
    private PatrolPoint[] _patrolPointsList;

    public UnityEvent OnEncounterStarted;
    public UnityEvent OnEncounterFinished;
    public UnityEvent OnUpdateObjected;

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

        // get the patrol point to the enemy controller
        _patrolPointsList = _patrolPoint.GetComponentsInChildren<PatrolPoint>();
        spawned.PatrolPoints = _patrolPointsList;

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

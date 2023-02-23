using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class Encounter : MonoBehaviour
{
    [SerializeField] private TriggerVolume _terminalPoint;
    private int _levelCounter = 0;                                 // count levels

    [SerializeField] private Transform _patrolPoint;
    private PatrolPoint[] _patrolPointsList;

    public List<EnemyController> CurrentEnemies = new List<EnemyController>();    // store enemies that spawned

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
        if (_terminalPoint.IsGameOver == true) return;

        EnemyController spawned = Instantiate(prefabs[_levelCounter], transform.position, transform.rotation) as EnemyController;

        // get the patrol point to the enemy controller
        _patrolPointsList = _patrolPoint.GetComponentsInChildren<PatrolPoint>();
        spawned.PatrolPoints = _patrolPointsList;

        // add to list for tracking and listen for death
        CurrentEnemies.Add(spawned);
    }

    // callback from enemies reaching the ending point
    public void RemoveEnemy(EnemyController enemy)
    {
        // remove enemy from the list
        if(CurrentEnemies.Contains(enemy))
        {
            CurrentEnemies.Remove(enemy);

            // level up
            if (CurrentEnemies.Count == 0 && !_terminalPoint.IsGameOver) _levelCounter++;
        }
    }
}

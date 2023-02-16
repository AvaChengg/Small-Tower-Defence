using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesEncounter : Encounter
{
    [Header("PatrolPoint Setting")]
    public EnemyController[] _monsters;     // spawn the monsters prefab
    public float _spawnInterval = 0.5f;     // spawn monster interval
    public int quantity = 10;               // spawn monster quantity

    public override void StartEncounter()
    {
        base.StartEncounter();

        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        for(int i = 0; i <= quantity - 1; i++)
        {
            SpawnEnemy(_monsters);

            yield return new WaitForSeconds(_spawnInterval);
        }
    }
}

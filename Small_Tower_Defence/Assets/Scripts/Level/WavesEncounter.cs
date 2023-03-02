using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesEncounter : Encounter
{
    [Header("Spawning Setting")]
    [SerializeField] private float _spawnInterval = 0.5f;     // spawn monster interval
    [SerializeField] private int _quantity = 10;              // spawn monster quantity
    public int Reward = 10;                                   // Reward money

    public override void StartEncounter()
    {
        if (_isWin) return;
        base.StartEncounter();

        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        for(int i = 0; i <= _quantity - 1; i++)
        {
            SpawnEnemy(_monsters);

            yield return new WaitForSeconds(_spawnInterval);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : LevelController
{
    private void Start()
    {
        GetPatrolPoints();
    }
    private void GetPatrolPoints()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform patrolPoint = transform.GetChild(i);
            _patrolPoints.Add(patrolPoint);
        }
    }
}

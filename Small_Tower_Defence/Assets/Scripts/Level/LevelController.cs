using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // level
    protected int _levelCounter = 0;        // count levels
    protected int _lives = 30;              // player lives
    protected bool _isGameOver = false;

    // Spawner
    protected int _pathIndex = 0;
    protected List<Transform> _patrolPoints;
}

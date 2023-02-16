using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // level
    [Header("Level Setting")]
    protected int _levelCounter = 0;                // count levels
    protected bool _isGameOver = false;
    [SerializeField] protected int _lives = 30;     // player lives
}

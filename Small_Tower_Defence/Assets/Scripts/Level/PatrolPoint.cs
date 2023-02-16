using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    // called before the first frame update
    [HideInInspector]
    public Vector3 Position => transform.position;
}

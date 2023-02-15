using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerVolume : MonoBehaviour
{
    [SerializeField] private string _tagFilter = "Enemy";

    public UnityEvent<GameObject> OnEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(_tagFilter) && !other.CompareTag(_tagFilter)) return;

        // kill monsters to start next level
        OnEnter.Invoke(other.gameObject);
    }

}

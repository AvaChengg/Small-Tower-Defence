using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _fillBar;

    private void Update()
    {
        // set current amount
        _fillBar.fillAmount = _health.Percentage;

        // fix orientation
        transform.rotation = Camera.main.transform.rotation;
    }
}

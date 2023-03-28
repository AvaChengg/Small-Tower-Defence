using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _placeBuilding;

    public void PlaceBuildingSFX()
    {
        _placeBuilding.Play();
    }
}

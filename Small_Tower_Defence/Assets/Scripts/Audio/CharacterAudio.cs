using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _footstepOne;
    [SerializeField] private AudioSource _footstepTwo;
    [SerializeField] private AudioSource _attack;

    public void AnimEventFootstepOne()
    {
        _footstepOne.Play();
    }
    public void AnimEventFootstepTwo()
    {
        _footstepTwo.Play();
    }
    public void PlayAttackSFX()
    {
        _attack.Play();
    }
}

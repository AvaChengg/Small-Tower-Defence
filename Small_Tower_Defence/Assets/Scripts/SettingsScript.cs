using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public Slider slider;

    void update()
    {
        slider.value = Mathf.MoveTowards(slider.value, 100.0f, 0.15f);
    }

    // public AudioMixer _mixer;
    //public TMP_Text _masterLable;
    //public Slider masterSlider;

    //sets audio slider volume
    /*public void SetMasterVolume()
    {
        _masterLable.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();

        theMixer.SetFloat("MasterVolume", masterSlider.value);
    }*/

    // [SerializeField] private Slider masterVolume = null;

}

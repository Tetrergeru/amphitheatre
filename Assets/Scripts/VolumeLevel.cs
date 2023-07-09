using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeLevel : MonoBehaviour
{
    public SoundScript SoundScript;
    public Slider Slider;

    void Start()
    {
        DontDestroyOnLoad(SoundScript.gameObject);
    }

    void Update()
    {
        SoundScript.Source.volume = Slider.value;
    }
}

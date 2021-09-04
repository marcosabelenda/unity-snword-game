using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AudioMenu : MonoBehaviour
{
    public Slider sliderBar;

    // Start is called before the first frame update
    void Start()
    {
        sliderBar.value = GameConfiguration.gameConfiguration.globalVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSound()
    {
        AudioListener.volume = sliderBar.value;
        GameConfiguration.gameConfiguration.globalVolume = sliderBar.value;
    }
}

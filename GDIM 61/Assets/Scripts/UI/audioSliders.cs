using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class audioSliders : MonoBehaviour
{
    
    public bool musicSlider;
    public bool sfxSlider;
    public Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        if(musicSlider)
        {
            slider.value = AudioManager.musicMod;
        }
        else if(sfxSlider)
        {
            
            slider.value = AudioManager.sfxMod;
        }
    }

    public void updateSoundLevel()
    {
        if(musicSlider)
        {
            AudioManager.instance.updateMusicLevel(slider.value);
        }
        else if(sfxSlider)
        {
            
            AudioManager.instance.updateSfxLevel(slider.value);
        }
        
    }

}

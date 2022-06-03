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
    }

    public void updateSoundLevel()
    {
        if(musicSlider)
        {
            AudioManager.instance.updateMusicLevel(slider.value);
        }
        else if(sfxSlider)
        {
            Debug.Log("AAAWA");
            AudioManager.instance.updateSfxLevel(slider.value);
        }
        
    }

}

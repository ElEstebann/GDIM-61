using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    //Brackeys: Introduction to AUDIO in Unity
    //https://youtu.be/6OT43pvUyfY
    //To access sounds: Sound s = AudioManager.instance.FindSound("sound name");
    //                  s.Play();
    //Or can use predefined:
    //                  AudioManager.instance.Play("sound name");

    [SerializeField] private AudioMixer master;
    [SerializeField] private AudioMixerGroup sfxGroup;
    public static float musicMod = 1f;
    public static float sfxMod = 1f;
    
    public Sound[] sounds;

    public static AudioManager instance;
    
    void Awake()    
    {
        DontDestroyOnLoad(gameObject);


        if (instance == null){
            instance = this;
            Debug.Log("Instance was null");
        } else if (instance != this){
            Destroy(gameObject);
            Debug.Log("Destroyed.");
            return;
        }


        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            if(!s.bgm)
            {
                s.source.outputAudioMixerGroup = sfxGroup;
            }
        }
    }

    //Find sound in sounds array
    public Sound FindSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound \"" + name + "\" not found!");
        }

        return s;
    }

    //Play a sound
    public Sound Play(string soundName)
    {
        Sound s = FindSound(soundName);

        if (s != null)
            s.source.Play();

        return s;
    }

    //Play a sound using PlayOneShot
    public Sound PlayOneShot(string soundName)
    {
        Sound s = FindSound(soundName);

        if (s != null)
            s.source.PlayOneShot(s.source.clip);

        return s;
    }

    //Stop a sound
    public Sound Stop(string soundName)
    {
        Sound s = FindSound(soundName);

        if (s != null)
            s.source.Stop();

        return s;
    }

    //Pause/Unpause sound
    public Sound Pause(string soundName, bool pause)
    {
        Sound s = FindSound(soundName);

        if (s != null)
        {
            if (pause)
                s.source.Pause();
            else
                s.source.UnPause();
        }

        return s;
    }

    public void updateMusicLevel(float modifier)
    {
        musicMod = modifier;
        foreach(Sound audio in sounds)
        {
            if(audio.bgm)
            {
                audio.source.volume = musicMod;
            }
        }
    }

    public void updateSfxLevel(float modifier)
    {
        sfxMod = modifier;
        /*foreach(Sound audio in sounds)
        {
            if(!audio.bgm)
            {
                audio.source.volume = sfxMod;
            }
        }*/

        master.SetFloat("MasterVolume", modifier);
    }

}

using System;
using UnityEngine.Audio;
using UnityEngine;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public Sound[] musicLibrary , sfxLibrary;
    public static AudioManager instance;
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        SetupSound(musicLibrary);
        SetupSound(sfxLibrary);

        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayMusic("Vinyl");
        PlayMusic("Theme");
        // PlaySFX("Walk");
    }

    void SetupSound(Sound[] soundsArray)
    {
        foreach(Sound s in soundsArray)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicLibrary, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogError("Music: "  + name + " not found");
            return;
        }

        s.source.Play();
    }

    public void StopMusic(string name)
    {
        Sound s = Array.Find(musicLibrary, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogError("Music: "  + name + " not found");
            return;
        }

        s.source.Stop();
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxLibrary, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogError("Sound: "  + name + " not found");
            return;
        }

        s.source.Play();
        // Debug.Log("Played SFX :" + name);
        // Debug.Log(s.clip.length);
    }

    public void StopSFX(string name)
    {
        Sound s = Array.Find(sfxLibrary, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogError("Sound: "  + name + " not found");
            return;
        }

        s.source.Stop();
    }
}

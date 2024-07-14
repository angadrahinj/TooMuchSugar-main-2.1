using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager instance;
    public Video[] cutscenes;
    public event Action OnCutsceneStarted;

    [SerializeField] PlayerInput playerInput;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }

        OnCutsceneStarted += SwitchToCutsceneInput;

        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }

    void Start()
    {
        SetupCutscenes(cutscenes);
    }

    void SetupCutscenes(Video[] cutsceneLibrary)
    {
        foreach(Video v in cutsceneLibrary)
        {
            v.source = gameObject.AddComponent<VideoPlayer>();
            v.source.playOnAwake = v.playOnAwake;

            if (v.playOnAwake)
            {
                OnCutsceneStarted?.Invoke();
                v.SpecialCutsceneStartEvents?.Invoke();
            }

            v.source.renderMode = v.renderMode;
            v.source.targetCamera = Camera.main;
            v.source.clip = v.clip;
            v.source.playbackSpeed = v.playBackSpeed;
            
            v.source.isLooping = v.loop;

            v.source.loopPointReached += CutsceneFinish;
        }
    }

    void CanvasToggle(bool value)
    {
        HealthBar.instance.healthBarSprite.enabled = value;
    }

    void SwitchToCutsceneInput()
    {
        playerInput.SwitchCurrentActionMap("Cutscene");
    }

    void SwitchToGameplayInput()
    {
        playerInput.SwitchCurrentActionMap("Gameplay");
    }

    public void PlayCutscene(string name)
    {
        Video v = Array.Find(cutscenes, video => video.name == name);

        if (v == null)
        {
            Debug.LogError("Cutscene: "  + name + " not found");
            return;
        }

        v.source.Play();
        //Generic Turn off controls : Standard for all cutscenes
        OnCutsceneStarted?.Invoke();
        //Special cutscenes that can call from the inspector => Player learning an ability etc
        v.SpecialCutsceneStartEvents?.Invoke();
    }

    void OnDisable()
    {
        foreach(Video v in cutscenes)
        {
            v.source.loopPointReached -= CutsceneFinish;
        }

        OnCutsceneStarted -= SwitchToCutsceneInput;
    }

    void CutsceneFinish(UnityEngine.Video.VideoPlayer vp)
    {
        Debug.Log("Movie has finished");
        vp.Stop();

        Video v = Array.Find(cutscenes, video => video.clip == vp.clip);
        v.SpecialCutsceneEndEvents?.Invoke();

        SwitchToGameplayInput();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

[System.Serializable]
public class Video
{
    public string name;
    [HideInInspector] public VideoPlayer source;
    public VideoClip clip;
    [Range(0.1f, 10f)]
    public float playBackSpeed = 1f;
    public VideoRenderMode renderMode;

    public UnityEvent SpecialCutsceneStartEvents;
    public UnityEvent SpecialCutsceneEndEvents;
    
    public bool playOnAwake = false;
    public bool loop = false;
}

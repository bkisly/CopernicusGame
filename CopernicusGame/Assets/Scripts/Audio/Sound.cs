using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip Clip;
    public AudioMixerGroup AudioMixerGroup;

    [Range(0f, 1f)]
    public float Volume;
    public bool IsLooping;

    [HideInInspector]
    public AudioSource Source;
}

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;
    [Space]
    public string LevelMusicName;

    private void Awake()
    {
        foreach(Sound sound in Sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();

            sound.Source.clip = sound.Clip;
            sound.Source.volume = sound.Volume;
            sound.Source.loop = sound.IsLooping;
            sound.Source.outputAudioMixerGroup = sound.AudioMixerGroup;
        }
    }

    private void Start()
    {
        print(Application.persistentDataPath);
        if(!string.IsNullOrWhiteSpace(LevelMusicName)) PlaySound(LevelMusicName);
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);
        s.Source.Play();
    }
}

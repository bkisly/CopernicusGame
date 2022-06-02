using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsConfigInfo
{
    public int ResolutionWidth;
    public int ResolutionHeight;
    public bool FullScreen;

    public float MasterVolume;
    public float MusicVolume;
    public float SFXVolume;
    public float VoiceVolume;

    public bool Subtitles;

    public SettingsConfigInfo(Resolution resolution, bool fullScreen, float masterVolume, float musicVolume, float sfxVolume, float voiceVolume, bool subtitles)
    {
        ResolutionWidth = resolution.width;
        ResolutionHeight = resolution.height;
        FullScreen = fullScreen;

        MasterVolume = masterVolume;
        MusicVolume = musicVolume;
        SFXVolume = sfxVolume;
        VoiceVolume = voiceVolume;

        Subtitles = subtitles;
    }
}

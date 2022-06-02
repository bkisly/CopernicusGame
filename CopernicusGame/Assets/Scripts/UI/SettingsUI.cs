using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public AudioMixer AudioMixer;
    public Dropdown ResolutionDropdown;
    public Toggle FullScreenToggle;
    public Toggle SubtitlesToggle;
    [Space]
    public Slider MasterVolumeSlider, MusicVolumeSlider, SFXVolumeSlider, VoiceVolumeSlider;

    private Resolution[] _resolutions;

    public void SetMasterVolume(float value)
    {
        AudioMixer.SetFloat("MasterVolume", value);
    }
    public void SetMusicVolume(float value)
    {
        AudioMixer.SetFloat("MusicVolume", value);
    }
    public void SetSFXVolume(float value)
    {
        AudioMixer.SetFloat("SFXVolume", value);
    }
    public void SetVoiceVolume(float value)
    {
        AudioMixer.SetFloat("VoiceVolume", value);
    }

    private void UpdateResolutionOptions()
    {
        _resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        ResolutionDropdown.ClearOptions();

        List<string> resolutionDropdownOptions = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < _resolutions.Length; i++)
        {
            resolutionDropdownOptions.Add(string.Format("{0}x{1}", _resolutions[i].width, _resolutions[i].height));

            if (_resolutions[i].width == Screen.width && _resolutions[i].height == Screen.height)
                currentResolutionIndex = i;
        }

        ResolutionDropdown.AddOptions(resolutionDropdownOptions);

        print("der");
        ResolutionDropdown.value = currentResolutionIndex;
    }

    public void SaveSettings()
    {
        AudioMixer.GetFloat("MasterVolume", out float masterVolume);
        AudioMixer.GetFloat("MusicVolume", out float musicVolume);
        AudioMixer.GetFloat("SFXVolume", out float sfxVolume);
        AudioMixer.GetFloat("VoiceVolume", out float voiceVolume);

        SettingsConfigInfo settingsConfigInfo = new SettingsConfigInfo(
            _resolutions[ResolutionDropdown.value],
            FullScreenToggle.isOn,
            masterVolume,
            musicVolume,
            sfxVolume,
            voiceVolume,
            SubtitlesToggle.isOn);

        Serializer.Serialize(settingsConfigInfo);
        ReadSettings();
    }

    public void ReadSettings()
    {
        SettingsConfigInfo settingsConfigInfo = Serializer.DeserializeSettings();
        if (settingsConfigInfo != null)
        {
            Screen.SetResolution(settingsConfigInfo.ResolutionWidth, settingsConfigInfo.ResolutionHeight, settingsConfigInfo.FullScreen);

            MasterVolumeSlider.value = settingsConfigInfo.MasterVolume;
            MusicVolumeSlider.value = settingsConfigInfo.MusicVolume;
            SFXVolumeSlider.value = settingsConfigInfo.SFXVolume;
            VoiceVolumeSlider.value = settingsConfigInfo.VoiceVolume;

            FullScreenToggle.isOn = settingsConfigInfo.FullScreen;
            SubtitlesToggle.isOn = settingsConfigInfo.Subtitles;
            print(string.Format("Current resolution is: {0}x{1}, fullscreen is: {2}", settingsConfigInfo.ResolutionWidth, settingsConfigInfo.ResolutionHeight, settingsConfigInfo.FullScreen));
        }

        UpdateResolutionOptions();
    }
}

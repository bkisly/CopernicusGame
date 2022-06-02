using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public GameObject ExitPanel;
    public GameObject InstructionsPanel;
    public GameObject SettingsPanel;

    private void Start()
    {
        SettingsPanel.SetActive(true);
        SettingsPanel.GetComponent<SettingsUI>().ReadSettings();
        SettingsPanel.SetActive(false);
    }

    public void NewGame()
    {
        Serializer.ResetGameSave();
        Continue();
    }

    public void Continue()
    {
        PlayClickSound();

        PlayerConfigInfo playerConfigInfo = Serializer.DeserializeSave();
        int sceneIndex = 1;
        if (playerConfigInfo != null) sceneIndex = playerConfigInfo.LevelIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public void Instructions(bool open)
    {
        PlayClickSound();
        InstructionsPanel.SetActive(open);
    }

    public void Settings(bool open)
    {
        PlayClickSound();
        SettingsPanel.SetActive(open);
    }

    public void Exit()
    {
        PlayClickSound();
        ExitPanel.SetActive(true);
    }

    public void ExitConfirmation(bool exit)
    {
        PlayClickSound();
        if (exit == true) Application.Quit();
        else
        {
            ExitPanel.SetActive(false);
        }
    }

    public void PlayHoverSound()
    {
        FindObjectOfType<AudioManager>().PlaySound("UIHover");
    }

    public void PlayClickSound()
    {
        FindObjectOfType<AudioManager>().PlaySound("UICheck");
    }
}

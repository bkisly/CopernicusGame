using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public GameObject PausePanel;
    private bool _pasued = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) TogglePause();   
    }

    public void TogglePause()
    {
        _pasued = !_pasued;
        PausePanel.SetActive(_pasued);
        if (_pasued == true)
        {
            Time.timeScale = 0;
            FindObjectOfType<LayoutsToggler>().DisableLayouts(typeof(PauseUI));
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1;
            FindObjectOfType<LayoutsToggler>().EnableLayouts(typeof(PauseUI));
            Cursor.visible = false;
        }

        PlayClickSound();
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlayClickSound()
    {
        FindObjectOfType<AudioManager>().PlaySound("UICheck");
    }

    public void PlayHoverSound()
    {
        FindObjectOfType<AudioManager>().PlaySound("UIHover");
    }
}

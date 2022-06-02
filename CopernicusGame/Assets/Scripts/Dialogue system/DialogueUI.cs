using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public DialogueManager DialogueManager;
    public Text Header, Text;

    public void NextDialogue()
    {
        if (!DialogueManager.NextDialogue()) UpdateContents();
        else DisableLayout();
    }

    private void UpdateContents()
    {
        Header.text = DialogueManager.CurrentDialogue.Header;
        Text.text = DialogueManager.CurrentDialogue.Text;
    }

    private void DisableLayout()
    {
        print("trololo");
        gameObject.SetActive(false);
    }

    public void DialogueStarted()
    {
        UpdateContents();
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

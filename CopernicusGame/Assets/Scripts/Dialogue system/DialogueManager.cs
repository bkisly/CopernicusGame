using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public List<Dialogue> Dialogues;
    private Queue<Dialogue> _dialogueQueue;
    public Dialogue CurrentDialogue { get; private set; }
    public Animator BossAnimator;
    public UnityEvent DialogueStartedEvent;

    private void Start()
    {  
        Time.timeScale = 0;
        FindObjectOfType<LayoutsToggler>().DisableLayouts();
        _dialogueQueue = new Queue<Dialogue>();
        StartDialogue();
        OnDialogueStarted();
    }

    public void StartDialogue()
    {
        foreach (Dialogue dialogue in Dialogues) _dialogueQueue.Enqueue(dialogue);
        print(_dialogueQueue.Count);
        NextDialogue();
    }

    public bool NextDialogue()
    {
        if (_dialogueQueue.Count > 0)
        {
            print("hei");
            CurrentDialogue = _dialogueQueue.Dequeue();
            return false;
        }
        else
        {
            OnDialogueEnded();
            return true;
        }
    }

    private void OnDialogueEnded()
    {
        print("hai");
        BossAnimator.SetTrigger("StartFight");
        Time.timeScale = 1;
        CurrentDialogue = null;
        Cursor.visible = false;
        FindObjectOfType<LayoutsToggler>().EnableLayouts();
    }

    private void OnDialogueStarted()
    {
        DialogueStartedEvent.Invoke();
    }
}

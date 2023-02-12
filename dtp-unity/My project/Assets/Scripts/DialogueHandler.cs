using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueHandler : MonoBehaviour
{
    TextHandler textHandler;
    Dialogue loadedDialogue;


    public void StartDialogue(Dialogue newDialogue)
    {
        loadedDialogue = newDialogue;
        textHandler = new TextHandler();
        textHandler.SetActionOnLetterAdded(() => OnLetterAdded());
        textHandler.SetActionOnTypactionOnTypeWriteCompletion(() => OnTypeWriteCompletion());
        loadedDialogue.ResetDialogue();
        InstantiateScene();
        PlayDirection();   
    }
    public void ContinueDialogue()
    {
        if(loadedDialogue != null)
        {
            RecieveAndLoadNextIndex();
            if(!loadedDialogue.CurrentDirectionMatchesIndex())
            {
                loadedDialogue.SetCurrentDirection();
                RefreshScene();
                PlayDirection();
            }
            else
            {
                RecieveInput(); 
            }
        }
    }
    public void EndDialogue()
    {
        textHandler = null;
        loadedDialogue = null;
    }

    public void RecieveAndLoadNextIndex()
    {
        int[] nextIndex = RecieveInput();
        if(nextIndex == null) loadedDialogue.SetIndexToNextAvailableDirection();
        else loadedDialogue.SetIndex(nextIndex);

    }

    public void PlayDirection()
    {
        ActiveCharacter speaking = loadedDialogue.GetCurrentDirection().GetTalking();
        textHandler.SetTextSpeedOnTypeWrite(speaking.TalkSpeed);
        textHandler.SetInputText(speaking.GetName() + ":\n");
        textHandler.AppendToText();
        textHandler.SetInputText(loadedDialogue.GetCurrentDirection().Text);
        StartCoroutine(textHandler.TypeWrite());
    }
    public void OnTypeWriteCompletion()
    {
        switch(loadedDialogue.GetCurrentDirection().GetType().Name)
        {
            case "Direction":
                //Output for normal Direction
                break;
            case "InputDirection":
                //Output for Input Direction
                break;
        }
    }

    public void OnLetterAdded()
    {
        PlaySoundFont();
    }

    public abstract void RefreshScene();
    public abstract void InstantiateScene();
    public abstract void PlaySoundFont();
    public abstract int[] RecieveInput();
}

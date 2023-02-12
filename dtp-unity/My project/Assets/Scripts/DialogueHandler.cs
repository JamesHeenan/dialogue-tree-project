using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class DialogueHandler : MonoBehaviour
{
    InputHandler inputHandler;
    TextHandler textHandler;
    Dialogue loadedDialogue;


    public void StartDialogue(Dialogue newDialogue)
    {
        loadedDialogue = newDialogue;
        inputHandler = new InputHandler();
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
                loadedDialogue.SetIndex(RecieveInput()); 
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
        textHandler.SetTextSpeedInput(speaking.TalkSpeed);
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
        if(!SpeedUpOnContinue())
        {
            textHandler.ReadPunctuation();
            PlaySoundFont();
        }
    }
    public bool SpeedUpOnContinue()
    {
        if(inputHandler.continueButton.InputPressed()) 
        {
            textHandler.SetTextSpeedInput(0.01f); 
            return true;           
        }
        else 
        {
            textHandler.SetTextSpeedInput(loadedDialogue.GetCurrentDirection().GetTalking().TalkSpeed);
            return false;
        }

    }

    public abstract void RefreshScene();
    public abstract void InstantiateScene();
    public abstract void PlaySoundFont();
    public int[] RecieveInput()
    {
        DirectionBase currentDirection = loadedDialogue.GetCurrentDirection();
        switch(currentDirection.GetType().Name)
        {
            case "Direction":
                if(inputHandler.continueButton.InputDown())
                    return currentDirection.GetIndexOutputs()[0];
                break;
            case "InputDirection":
                //Output for Input Direction
                break;
        }
    }
}

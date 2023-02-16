using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class DialogueHandler : MonoBehaviour
{
    InputHandler inputHandler;
    TextHandler textHandler;
    SceneHandler sceneHandler;
    Dialogue loadedDialogue;

    public void StartDialogue(Dialogue newDialogue)
    {
        //Load Dialogue and Handlers
        loadedDialogue = newDialogue;
        //inputHandler = GetComponent<InputHandler>();
        textHandler = GetComponent<TextHandler>();
        sceneHandler = GetComponent<SceneHandler>();
        inputHandler = GetComponent<InputHandler>();

        //Set Up Text Handler
        textHandler.SetActionOnLetterAdded(() => OnLetterAdded());
        textHandler.SetActionOnTypactionOnTypeWriteCompletion(() => OnTypeWriteCompletion());

        //Reset the Dialogue properties
        loadedDialogue.ResetDialogue();

        //Create the Scene probably should be in a scene handler
        sceneHandler.ResetScene();
        sceneHandler.RefreshScene(loadedDialogue.CurrentDirection);
        PlayDirection();
    }
    public void ContinueDialogue()
    {
        if(!loadedDialogue.GetDirectionActive()) //if direction isn't active whilst direction matches the index
        {
            Index nextIndex = RecieveInput();
            if(nextIndex != null)
            {
                if(nextIndex.GetScript() == -1) loadedDialogue.SetIndexToNextAvailableDirection();
                else loadedDialogue.SetIndex(nextIndex);
                loadedDialogue.SetCurrentDirectionFromIndex();
                sceneHandler.RefreshScene(loadedDialogue.CurrentDirection);
                textHandler.AppendToText("\n \n");
                PlayDirection();
            }

        }
    }
    public void EndDialogue()
    {
        textHandler = null;
        loadedDialogue = null;
    }
    public void PlayDirection()
    {
        loadedDialogue.SetDirectionActive(true); //Set active while in play
        Debug.Log("loaded dialogue: " + loadedDialogue.CurrentDirection);
        textHandler.SetTextSpeedInput(loadedDialogue.CurrentDirection.GetTalking().Character.TalkSpeed);
        textHandler.AppendToText(loadedDialogue.CurrentDirection.GetTalking().GetName() + ":\n");
        sceneHandler.RefreshText(textHandler.GetOutputText());
        Debug.Log(loadedDialogue.CurrentDirection.Text);
        StartCoroutine(textHandler.TypeWrite(loadedDialogue.CurrentDirection.Text));
    }
    public void OnTypeWriteCompletion()
    {
        switch(loadedDialogue.CurrentDirection.GetType().Name)
        {
            case "Direction":
                textHandler.AppendToText("\n\n1: Continue");
                break;
            case "InputDirection":
                InputDirection iD = (InputDirection)loadedDialogue.CurrentDirection;
                textHandler.AppendToText("\n");
                for(int i = 0; i < iD.Inputs.Length; i++) //write out inputs
                    textHandler.AppendToText("\n" + (i+1) + ": " + iD.Inputs[i]);
                break;
        }
        sceneHandler.RefreshText(textHandler.GetOutputText());
        loadedDialogue.SetDirectionActive(false); //On end of Play set false
    }
    public void OnLetterAdded()
    {
        sceneHandler.RefreshText(textHandler.GetOutputText());
        sceneHandler.MoveScrollbarTo(0);
        if(!SpeedUpOnContinue())
        {
            sceneHandler.PlaySoundFont();
        }
    }
    public bool SpeedUpOnContinue()
    {
        if(inputHandler.ContinueDown()) 
        {
            textHandler.SetTextSpeedInput(0.01f); 
            return true;           
        }
        else 
        {
            textHandler.SetTextSpeedInput(loadedDialogue.CurrentDirection.GetTalking().Character.TalkSpeed);
            return false;
        }

    }
    public Index RecieveInput()
    {
        DirectionBase currentDirection = loadedDialogue.CurrentDirection;
        Index index = null;
            switch(currentDirection.GetType().Name)
            {
                case "InputDirection":
                    if(inputHandler.GetNumericalInput() > 0)
                    {
                        index = currentDirection.GetIndexOutputs()[inputHandler.GetNumericalInput()-1];
                    }
                    break;
                default: //normal Direction
                    if(inputHandler.ContinueDown()) 
                    {
                        index = currentDirection.GetIndexOutputs()[0];     
                    }
                    break;
            }
        return index;
    }
}

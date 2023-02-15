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
        inputHandler = GetComponent<InputHandler>();
        textHandler = GetComponent<TextHandler>();
        sceneHandler = GetComponent<SceneHandler>();

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
            RecieveAndLoadNextDirection();
            sceneHandler.RefreshScene(loadedDialogue.CurrentDirection);
            PlayDirection();
        }
    }
    public void EndDialogue()
    {
        textHandler = null;
        loadedDialogue = null;
    }
    public void RecieveAndLoadNextDirection()
    {
        int[] nextIndex = RecieveInput();
        if(nextIndex == null) loadedDialogue.SetIndexToNextAvailableDirection();
        else loadedDialogue.SetIndex(nextIndex);
        loadedDialogue.SetCurrentDirectionFromIndex();
    }
    public void PlayDirection()
    {
        loadedDialogue.SetDirectionActive(true); //Set active while in play
        Debug.Log("loaded dialogue: " + loadedDialogue.CurrentDirection);
        textHandler.SetTextSpeedInput(loadedDialogue.CurrentDirection.Characters[loadedDialogue.CurrentDirection.TalkingPos].Character.TalkSpeed);
        textHandler.SetInputText(loadedDialogue.CurrentDirection.Characters[loadedDialogue.CurrentDirection.TalkingPos].GetName() + ":\n");
        textHandler.AppendToText();
        textHandler.SetInputText(loadedDialogue.CurrentDirection.Text);
        StartCoroutine(textHandler.TypeWrite());
    }
    public void OnTypeWriteCompletion()
    {
        switch(loadedDialogue.CurrentDirection.GetType().Name)
        {
            case "Direction":
                //Output for normal Direction
                break;
            case "InputDirection":
                InputDirection iD = (InputDirection)loadedDialogue.CurrentDirection;
                textHandler.SetInputText("\n");
                textHandler.AppendToText();
                for(int i = 0; i < iD.Inputs.Length; i++) //write out inputs
                {
                    textHandler.SetInputText("\n" + (i+1) + ": " + iD.Inputs[i]);
                    textHandler.AppendToText();
                }
                break;
        }
        loadedDialogue.SetDirectionActive(false); //On end of Play set false
    }
    public void OnLetterAdded()
    {
        if(!SpeedUpOnContinue())
        {
            sceneHandler.RefreshText(textHandler.GetOutputText());
            textHandler.ReadPunctuation();
            sceneHandler.PlaySoundFont();
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
            textHandler.SetTextSpeedInput(loadedDialogue.CurrentDirection.Characters[loadedDialogue.CurrentDirection.TalkingPos].Character.TalkSpeed);
            return false;
        }

    }
    public int[] RecieveInput()
    {
        DirectionBase currentDirection = loadedDialogue.CurrentDirection;
        while(true)
        {
            switch(currentDirection.GetType().Name)
            {
                case "InputDirection":
                    if(inputHandler.GetNumericalInput() > 0)
                        return currentDirection.GetIndexOutputs()[inputHandler.GetNumericalInput()-1];
                    break;
                default: //normal Direction
                    if(inputHandler.continueButton.InputDown()) 
                        return currentDirection.GetIndexOutputs()[0];     
                    break;
            }
        }
    }
}

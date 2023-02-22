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
        loadedDialogue.ResetDialogue();
        //inputHandler = GetComponent<InputHandler>();
        textHandler = GetComponent<TextHandler>();
        sceneHandler = GetComponent<SceneHandler>();
        inputHandler = GetComponent<InputHandler>();

        //Set Up Text Handler
        textHandler.SetActionOnLetterAdded(() => OnLetterAdded());
        textHandler.SetActionOnTypactionOnTypeWriteCompletion(() => OnTypeWriteCompletion());

        //Create the Scene probably should be in a scene handler
        sceneHandler.ResetScene();
        //Reset the Dialogue properties
        loadedDialogue.SetDirectionActive(true);
        PlayDirection();
    }
    public void ContinueDialogue()
    {
        if(!loadedDialogue.GetDirectionActive()) //if direction isn't active whilst direction matches the index
        {
            ToggleOnTab();
            Index nextIndex = RecieveInput();
            if(nextIndex != null)
            {
                if(nextIndex.GetScript() == -1) loadedDialogue.SetIndexToNextAvailableDirection();
                else loadedDialogue.SetIndex(nextIndex);
                loadedDialogue.SetCurrentDirectionFromIndex();
                //Set textHandler to log instead of Visuals
                textHandler.SetOutputText(sceneHandler.log);
                sceneHandler.RefreshText(textHandler.GetOutputText());
                PlayDirection();
            }

        }
        else sceneHandler.ToggleTextBoxSize(false); // collapse text box
    }
    public void EndDialogue()
    {
        textHandler = null;
        loadedDialogue = null;
    }
    public void PlayDirection()
    {
        loadedDialogue.SetDirectionActive(true); //Set active while in play
        string nameType = loadedDialogue.CurrentDirection.GetType().Name;
        Debug.Log("loaded dialogue: " + loadedDialogue.CurrentDirection + "Of Type: " + nameType);
        switch(nameType)
        {
            case "SceneDirection":
                //refresh scene elements from Scene Direction
                SceneDirection sceneDirection = (SceneDirection)loadedDialogue.CurrentDirection;
                sceneHandler.RefreshScene(sceneDirection);
                loadedDialogue.SetDirectionActive(false); //End of Play
                break;
            case "TextDirection":
                TextDirection textDirection = (TextDirection)loadedDialogue.CurrentDirection;
                //Update and Show Visual Script
                textHandler.SetTextSpeedInput(sceneHandler.loadedSceneDirection.GetTalking().Character.TalkSpeed);
                textHandler.AppendToText(sceneHandler.loadedSceneDirection.GetTalking().GetName() + ":\n");
                sceneHandler.RefreshText(textHandler.GetOutputText());
                //Update Log
                sceneHandler.AppendToLog(sceneHandler.loadedSceneDirection.GetTalking().GetName() + ": ");
                Debug.Log(textDirection.Text);
                //Update Log
                sceneHandler.AppendToLog(textDirection.Text);
                //Update Typewrite
                StartCoroutine(textHandler.TypeWrite(textDirection.Text));
                break;
            case "TransitionDirection":
                TransitionDirection transistionDirection = (TransitionDirection)loadedDialogue.CurrentDirection;
                switch(transistionDirection.TransitionType)
                    {
                        case TransitionType.SetOverlayAlpha:
                            sceneHandler.SetOverlayAlpha(transistionDirection.alphaLevel);
                            loadedDialogue.SetDirectionActive(false);
                            break;
                        case TransitionType.FadeInToScene:
                            StartCoroutine(sceneHandler.FadeInToScene(transistionDirection.DurationOfTransition,transistionDirection.DurationDelay, ()=> {
                                loadedDialogue.SetDirectionActive(false); //On end of Play set false
                            }));
                            break;
                        case TransitionType.FadeToBlack:
                            StartCoroutine(sceneHandler.FadeToBlack(transistionDirection.DurationOfTransition, transistionDirection.DurationDelay, ()=> {
                                loadedDialogue.SetDirectionActive(false); //On end of Play set false
                            }));
                            break;
                    }
                break;
        }

        //If direction has a text input

    }
    public void OnTypeWriteCompletion()
    {
        switch(loadedDialogue.CurrentDirection.GetType().Name)
        {
            case "InputDirection":
                InputDirection tmp = (InputDirection)loadedDialogue.CurrentDirection;
                textHandler.AppendToText("\n");
                for(int i = 0; i < tmp.Inputs.Length; i++) //write out inputs
                    textHandler.AppendToText("\n" + (i+1) + ": " + tmp.Inputs[i]);
                break;
            default:
                textHandler.AppendToText("\n\n1: Continue");
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
        if(inputHandler.ButtonHeld("continue")) 
        {
            textHandler.SetTextSpeedInput(0); 
            return true;           
        }
        else 
        {
            textHandler.SetTextSpeedInput(sceneHandler.loadedSceneDirection.GetTalking().Character.TalkSpeed);
            return false;
        }

    }
    public Index RecieveInput()
    {
        Direction currentDirection = loadedDialogue.CurrentDirection;
        Index index = null;
            switch(currentDirection.GetType().Name)
            {
                case "InputDirection":
                    int input = inputHandler.GetNumericalInput();
                    if(input > -1 && input < currentDirection.GetIndexOutputs().Length)
                    {
                        InputDirection tmp = (InputDirection)currentDirection;
                        sceneHandler.AppendToLog( "\n\n[You]" + ": " + tmp.Inputs[input] + "\n \n");
                        index = currentDirection.GetIndexOutputs()[input];
                    }
                    break;
                case "TransitionDirection":
                case "SceneDirection":
                    index = currentDirection.GetIndexOutputs()[0];
                    break;
                default: //normal Direction
                    if(inputHandler.ButtonDown("continue")) 
                    {
                        sceneHandler.AppendToLog("\n \n");
                        index = currentDirection.GetIndexOutputs()[0];     
                    }
                    break;
            }
        return index;
    }

    public void ToggleOnTab()
    {
        if(inputHandler.ButtonHeld("tab"))
            sceneHandler.ToggleTextBoxSize(true);
        else sceneHandler.ToggleTextBoxSize(false);
    }
}

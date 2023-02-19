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
        sceneHandler.RefreshScene(loadedDialogue.CurrentDirection);
        //Reset the Dialogue properties
        loadedDialogue.SetDirectionActive(true);
        StartCoroutine(sceneHandler.FadeInToScene(1f,0,()=> {
                    PlayDirection();
        }));
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
                //Set textHandler to log instead of Visuals
                textHandler.SetOutputText(sceneHandler.log);
                sceneHandler.RefreshText(textHandler.GetOutputText());
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
        switch(loadedDialogue.CurrentDirection.GetType().Name)
        {
            case "Direction":
            case "InputDirection":
                Debug.Log("loaded dialogue: " + loadedDialogue.CurrentDirection);
                DirectionTextBase DirectionTextBase = (DirectionTextBase)loadedDialogue.CurrentDirection;
                //Update and Show Visual Script
                textHandler.SetTextSpeedInput(DirectionTextBase.GetTalking().Character.TalkSpeed);
                textHandler.AppendToText(DirectionTextBase.GetTalking().GetName() + ":\n");
                sceneHandler.RefreshText(textHandler.GetOutputText());
                //Update Log
                sceneHandler.AppendToLog(DirectionTextBase.GetTalking().GetName() + ":\n");
                Debug.Log(DirectionTextBase.Text);
                //Update Log
                sceneHandler.AppendToLog(DirectionTextBase.Text);
                //Update Typewrite
                StartCoroutine(textHandler.TypeWrite(DirectionTextBase.Text));
                break;
            case "TransitionDirection":
                TransitionDirection TransistionDirection = (TransitionDirection)loadedDialogue.CurrentDirection;
                switch(TransistionDirection.TransitionType)
                    {
                        case TransitionType.FadeInToScene:
                            StartCoroutine(sceneHandler.FadeInToScene(TransistionDirection.DurationOfTransition,TransistionDirection.DurationDelay, ()=> {
                                loadedDialogue.SetDirectionActive(false); //On end of Play set false
                            }));
                            break;
                        case TransitionType.FadeToBlack:
                            StartCoroutine(sceneHandler.FadeToBlack(TransistionDirection.DurationOfTransition, TransistionDirection.DurationDelay, ()=> {
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
            case "Direction":
                textHandler.AppendToText("\n\n1: Continue");
                break;
            case "InputDirection":
                InputDirection tmp = (InputDirection)loadedDialogue.CurrentDirection;
                textHandler.AppendToText("\n");
                for(int i = 0; i < tmp.Inputs.Length; i++) //write out inputs
                    textHandler.AppendToText("\n" + (i+1) + ": " + tmp.Inputs[i]);
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
        if(inputHandler.ButtonPressed("continue")) 
        {
            textHandler.SetTextSpeedInput(0); 
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
                    Debug.Log("input direction detected");
                    int input = inputHandler.GetNumericalInput();
                    if(input > -1 && input < currentDirection.GetIndexOutputs().Length)
                    {
                        InputDirection tmp = (InputDirection)currentDirection;
                        sceneHandler.AppendToLog("\n" + (input+1) + ": " + tmp.Inputs[input]);
                        index = currentDirection.GetIndexOutputs()[input];
                    }
                    break;
                case "TransitionDirection":
                    index = currentDirection.GetIndexOutputs()[0];
                    break;
                default: //normal Direction
                    if(inputHandler.ButtonDown("continue")) 
                    {
                        index = currentDirection.GetIndexOutputs()[0];     
                    }
                    break;
            }
        return index;
    }
}

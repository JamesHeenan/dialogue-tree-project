using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class DialogueReader : MonoBehaviour
{   
    public string outputText = "";

    public string outputName = "";
    public float textSpeed;
    public float baseTextSpeed;
    public Location location;
    public ActiveCharacter[] charactersInScene;
    public ActiveCharacter Talking;
    int ScriptNumber = -1;
    int DirectionNumber = 0;
    
    bool DialogueStarted = false;
    bool InputReady = false;
    
    public void StartDialogue(Dialogue dialogue)
    {
        InstantiateScene();
        DialogueStarted = true;
        ScriptNumber = 0;
        DirectionNumber = 0;
        PlayAndFindNextPosition(dialogue.DialogueTree);    
    }

    public void ContinueDialogue(Dialogue dialogue)
    {
        if(!DialogueStarted)
        {
            StartDialogue(dialogue);       
        }
        else
        {
            PlayAndFindNextPosition(dialogue.DialogueTree);
        }
    }

    public void PlayAndFindNextPosition(Script[] script)
    {    
        PlayDirection(script[ScriptNumber].Directions[DirectionNumber]);
        if(DirectionNumber < script[ScriptNumber].Directions.Length -1)
            DirectionNumber++;
        else
            ScriptNumber++;
    }

    public void PlayDirection(DirectionBase direction)
    {
        //Takes DirectionBase Generic and outputs specific Direction Type.
        switch(direction.GetType().Name)
        {
            case "Direction":
                PlayDirection((Direction)direction);
                break;
            case "InputDirection":
                PlayDirection((InputDirection)direction);
                break;
        }
    }

    public void PlayDirection(Direction direction)
    {
        Debug.Log("Direction played in direction of type:" + direction.GetType());
        PlayDirectionBase(direction, ()=> 
        { 
            if(direction.output != -1)
                ScriptNumber = direction.output;
            InputReady = true;
        });
    }
    public void PlayDirection(InputDirection inputDirection)
    {
        Debug.Log("Direction played in inputDirection of type:" + inputDirection.GetType());
        PlayDirectionBase(inputDirection, () => 
        {
            StartCoroutine(OutputOptionsText(inputDirection.Inputs, ()=>
            {
                InputReady = true;
            }));
        });

    }

    public void PlayDirectionBase(DirectionBase direction, Action action)
    {
        InputReady = false;
        location = direction.Location;
        charactersInScene = direction.Characters;
        RefreshScene();

        if(outputText != "")
            outputText += "\n";
        outputText += outputName + ":\n";  
        StartCoroutine(OutputText(direction.Text, action));
    }

    public IEnumerator OutputText(string text, Action action)
    {
        StartCoroutine(SkipTextAnimation());
        yield return new WaitForSeconds(1f);
        foreach(char letter in text.ToCharArray())
        {
            outputText += letter;
            Debug.Log(outputText);
            PlaySoundFont();
            if(letter == '.' || letter == '!')
                yield return new WaitForSeconds(textSpeed*25f);
            if(letter == ',')
                yield return new WaitForSeconds(textSpeed*12.5f);
            yield return new WaitForSeconds(textSpeed);
        }
        action();
        StopAllCoroutines();
    }
    public IEnumerator OutputOptionsText(string[] Inputs, Action action)
    {
        for (int i = 0; i < Inputs.Length; i++)
        {
            outputText += "\n" + (i+1) + ": " + Inputs[i];
        } 
        yield return null;
        action();
        StopAllCoroutines();
    }
    public abstract void RefreshScene();
    public abstract void InstantiateScene();
    public abstract void PlaySoundFont();
    public abstract IEnumerator SkipTextAnimation();

}


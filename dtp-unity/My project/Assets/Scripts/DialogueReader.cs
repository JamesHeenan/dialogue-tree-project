using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class DialogueReader : MonoBehaviour
{   
    public float NextPressed;
    public int NumericalInput;
    public string outputText = "";

    public string outputName = "";
    public float textSpeed;
    public float baseTextSpeed;
    public Location location;
    public ActiveCharacter[] charactersInScene;
    public ActiveCharacter Talking;
    int ScriptNumber = -1;
    int DirectionNumber = 0;
    public bool DirectionComplete = false;
    public bool NextDirectionReady = false;
    Type DirectionType;
    int[] Outputs;
    int TotalOptions;
    
    public void StartDialogue(Dialogue dialogue)
    {
        InstantiateScene();
        ScriptNumber = 0;
        DirectionNumber = 1;
        PlayDirection(dialogue.DialogueTree[0].Directions[0]);   
    }

    public void ContinueDialogue(Dialogue dialogue)
    {
        if(NextDirectionReady)
        {
            NextDirectionReady = false;
            FindPlayAndStoreNextPosition(dialogue.DialogueTree);
        }
    }

    public void FindPlayAndStoreNextPosition(Script[] script)
    {   
        NextDirectionReady = false;
        switch(DirectionType.Name)
        {
            case "Direction":
                if(Outputs[0] != -1) DirectionNumber = Outputs[0];                
                break;
            case "InputDirection":
                ScriptNumber = Outputs[NumericalInput-1];
                DirectionNumber = 0;
                break;
        }    
        PlayDirection(script[ScriptNumber].Directions[DirectionNumber]);     
        if(DirectionNumber < script[ScriptNumber].Directions.Length -1)
            DirectionNumber++;
        else
            ScriptNumber++;
    }

    public void PlayDirection(DirectionBase direction)
    {
        DirectionComplete = false;
        DirectionType = direction.GetType();
        location = direction.Location;
        charactersInScene = direction.Characters;
        RefreshScene();
        if(outputText != "")
            outputText += "\n";
        outputText += outputName + ":\n";  
        StartCoroutine(TypeWrite(direction.Text, () => 
        {
            switch(DirectionType.Name)
            {
                case "Direction":
                    PlayDirection((Direction)direction);
                    break;
                case "InputDirection":
                    PlayDirection((InputDirection)direction);
                    break;
            }
            DirectionComplete = true;
        }));
    }

    public void PlayDirection(Direction direction)
    {
        Debug.Log("Direction played in direction of type:" + direction.GetType());
        Outputs = new int[] {direction.output};
    }
    public void PlayDirection(InputDirection inputDirection)
    {
        Debug.Log("Direction played in inputDirection of type:" + inputDirection.GetType());
        string[] Inputs = inputDirection.Inputs;
        for (int i = 0; i < Inputs.Length; i++) AppendToText("\n" + (i+1) + ": " + Inputs[i]);
        Outputs = inputDirection.outputs;
        TotalOptions = Inputs.Length;
        
    }

    public IEnumerator TypeWrite(string text, Action action)
    {
        yield return new WaitForSeconds(1f);
        foreach(char letter in text.ToCharArray())
        {
            SpeedUpText();
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

    public void SpeedUpText()
    {
        if(NextPressed > 0)
        {
            textSpeed = 0.25f * baseTextSpeed;
        }
        else
        {
            textSpeed = baseTextSpeed;
        }
    }
    public void AppendToText(string input)
    {
        outputText += input;
    }
    public abstract void RefreshScene();
    public abstract void InstantiateScene();
    public abstract void PlaySoundFont();
    public abstract void AssignNextButton();

    public abstract void AssignNumericalInput();

    public void DirectionTrigger() 
    {
        if(DirectionComplete == true)
        {
            switch(DirectionType.Name)
            {
                case "Direction":
                    Debug.Log("Direction Ready");
                    if(NextPressed > 0) NextDirectionReady = true;
                    break;
                case "InputDirection":
                    Debug.Log("InputDirection Ready");
                    if(NumericalInput > 0 && NumericalInput <= TotalOptions) NextDirectionReady = true;
                    break;
            }
        }
    }

}


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
    int j = -1;
    public void StartDialogue(Dialogue dialogue)
    {
        int j = -1;
        InstantiateScene();
        for (int i = 0; i < dialogue.DialogueTree.Length; i++)
        {
            ReadScript(dialogue.DialogueTree[i]);
            if(j > -1) i = j-1;
        }
    }
    public void ReadScript(Script script)
    {
        j = -1;
        for (int i = 0; i < script.Directions.Length; i++)
        {
            PlayDirection((InputDirection)script.Directions[i]);
            if(j > -1) break;
        }        
    }

    public void PlayDirection(InputDirection direction)
    {
        PlayDirectionBase((Direction)direction, () => 
        {
            if(direction is InputDirection)
            { 
                StartCoroutine(OutputOptions(direction.Inputs));
                j =AwaitInput();
            }
            else
            {
                Continue();
                j = -1;
            }
        });

    }

    // public int PlayDirection(Direction direction)
    // {
    //     PlayDirectionBase(direction);
    //     Continue();
    //     return -1;
    // }
    private int AwaitInput()
    {
        throw new NotImplementedException();
    }

    public void PlayDirectionBase(Direction direction, Action action)
    {
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
    public IEnumerator OutputOptions(string[] Inputs)
    {
        for (int i = 0; i < Inputs.Length; i++)
        {
            outputText += "\n" + (i+1) + ": " + Inputs[i];
        } 
        yield return null;
    }
    public abstract void RefreshScene();
    public abstract void InstantiateScene();
    public abstract void PlaySoundFont();
    public abstract IEnumerator SkipTextAnimation();
    public abstract IEnumerator Continue();

}


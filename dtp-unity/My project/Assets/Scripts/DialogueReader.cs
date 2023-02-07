using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DialogueReader : MonoBehaviour
{   
    public string outputText;

    public float textSpeed;
    public float pressed;
    public Location location;
    public ActiveCharacter[] charactersInScene;
    public void ReadDialogue(Dialogue dialogue)
    {
        NewScene();
        Script baseScript = dialogue.DialogueTree[0];
    }
    public void ReadScript(Script script)
    {
        
    }

    public void PlayDirection(Direction direction)
    {
        location = direction.Location;
        charactersInScene = direction.Characters;
        RefreshScene();
        if(outputText != null)
            outputText += "\n";
        string outputName = "?????";
        foreach(ActiveCharacter chararacter in direction.Characters)
            if(chararacter.Talking)
            {
                outputName = chararacter.Name;
                textSpeed = chararacter.Character.TalkSpeed;
                break;
            }
        outputText += outputName + ":\n";
        StopAllCoroutines();
    }

    public IEnumerator SkipAnim()
    {
        if(pressed > 0)
        {
            textSpeed = 0;
            StopCoroutine(SkipAnim());
        }
        yield return null;
    }

    public IEnumerator OutputDirectionText(Direction direction)
    {
        StartCoroutine(SkipAnim());
        yield return new WaitForSeconds(1f);
        foreach(char letter in direction.Text.ToCharArray())
        {
            outputText += letter;
            PlaySoundFont();
            if(letter == '.' || letter == '!')
                yield return new WaitForSeconds(textSpeed*25f);
            if(letter == ',')
                yield return new WaitForSeconds(textSpeed*12.5f);
            yield return new WaitForSeconds(textSpeed);
        }
        StopCoroutine(OutputDirectionText(direction));
        StopCoroutine(SkipAnim());
    }
    public void OutputDirectionText(InputDirection inputDirection, string output)
    {
        OutputDirectionText(direction : inputDirection);

        for (int i = 0; i < inputDirection.Inputs.Length; i++)
        {
            CreateLink(inputDirection.Inputs[i]);
        }
    }
    public void CreateLink(string input)
    {

    }
    public void RefreshScene()
    {
        //left blank for abstraction
    }
    public void NewScene()
    {
        //left blank for abstraction
    }
    public void PlaySoundFont()
    {
        //left blank for abstraction
    }

}


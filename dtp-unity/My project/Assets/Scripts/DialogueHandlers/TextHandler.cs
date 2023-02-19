using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    float SpeedInputPressed;
    string outputText;
    char letter;
    float textSpeedInput;
    float textSpeedModifier;
    Action actionOnLetterAdded;
    Action actionOnTypeWriteCompletion;

    public void SetSpeedInputPressed(float input)
    {
        SpeedInputPressed = input;
    }
    public string GetOutputText()
    {
        return outputText;
    }
    public void SetOutputText(string input)
    {
        outputText = input;
    }

    public void SetTextSpeedInput(float input)
    {
        textSpeedInput = input;
    }
    public void SetActionOnLetterAdded(Action action)
    {
        actionOnLetterAdded = action;
    }


    public void AppendToText(string input)
    {
        outputText += input;
    }
    public void SetActionOnTypactionOnTypeWriteCompletion(Action action)
    {
        actionOnTypeWriteCompletion = action;
    }
    public IEnumerator TypeWrite(string input)
    {
        foreach(char c in input.ToCharArray())
        {
            letter = c;
            outputText += c;
            actionOnLetterAdded();
            ReadPunctuation();
            yield return new WaitForSecondsRealtime(textSpeedInput*textSpeedModifier);
        }
        actionOnTypeWriteCompletion();
    }

    public void ReadPunctuation()
    {
        if(letter == '.' || letter == '!' || letter == '?') textSpeedModifier = 12f;
        else if(letter == ';' || letter == ':') textSpeedModifier = 9f;
        else if(letter == ',') textSpeedModifier = 6f;
        else textSpeedModifier = 1;
    }
}

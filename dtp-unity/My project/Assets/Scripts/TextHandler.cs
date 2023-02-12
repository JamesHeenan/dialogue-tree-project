using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    float SpeedInputPressed;
    string outputText;
    string inputText;
    char letter;
    float textSpeedOnTypeWrite;
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
    public void SetInputText(string input)
    {
        inputText = input;
    }

    public void SetTextSpeedOnTypeWrite(float input)
    {
        textSpeedOnTypeWrite = input;
    }
    public void SetActionOnLetterAdded(Action action)
    {
        actionOnLetterAdded = action;
    }


    public void AppendToText()
    {
        outputText += inputText;
    }
    public void SetActionOnTypactionOnTypeWriteCompletion(Action action)
    {
        actionOnTypeWriteCompletion = action;
    }
    public IEnumerator TypeWrite()
    {

        foreach(char c in inputText.ToCharArray())
        {
            letter = c;
            actionOnLetterAdded();
            SpeedUpText();
            outputText += c;
            ReadPunctuation();
            yield return new WaitForSeconds(textSpeedOnTypeWrite*textSpeedModifier);
        }
        actionOnTypeWriteCompletion();
    }

    void ReadPunctuation()
    {
        if(letter == '.' || letter == '!' || letter == '?') textSpeedModifier = 25f;
        else if(letter == ',') textSpeedModifier = 12.5f;
        else textSpeedModifier = 0;
    }

    void SpeedUpText()
    {
        if(SpeedInputPressed > 0)
        {
            textSpeedOnTypeWrite = 0.25f * textSpeedInput;
        }
        else
        {
            textSpeedOnTypeWrite = textSpeedInput;
        }
    }
}

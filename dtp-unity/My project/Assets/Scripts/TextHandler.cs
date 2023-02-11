using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    float SpeedInput;
    public string outputText;
    public string input;
    float textSpeedOnTypeWrite;
    public float textSpeedInput;
    Action actionOnLetterAdded;
    Action actionOnTypeWriteCompletion;

    public void AppendToText()
    {
        outputText += input;
    }
    public IEnumerator TypeWrite()
    {
        yield return new WaitForSeconds(1f);
        foreach(char letter in input.ToCharArray())
        {
            actionOnLetterAdded();
            SpeedUpText();
            outputText += letter;
            Punctuation(letter);
            yield return new WaitForSeconds(textSpeedOnTypeWrite);
        }
        actionOnTypeWriteCompletion();
    }

    IEnumerator Punctuation(char letter)
    {
        if(letter == '.' || letter == '!' || letter == '?')
            yield return new WaitForSeconds(textSpeedOnTypeWrite*25f);
        if(letter == ',')
            yield return new WaitForSeconds(textSpeedOnTypeWrite*12.5f);
        yield return new WaitForSeconds(textSpeedOnTypeWrite);
    }

    

    void SpeedUpText()
    {
        if(SpeedInput > 0)
        {
            textSpeedOnTypeWrite = 0.25f * textSpeedInput;
        }
        else
        {
            textSpeedOnTypeWrite = textSpeedInput;
        }
    }
}

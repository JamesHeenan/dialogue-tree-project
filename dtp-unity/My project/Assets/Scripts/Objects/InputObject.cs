using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputObject
{
    bool inputDown;
    bool inputPressed;
    bool inputUp;
    bool inputPressedLastFrame = false;
    public abstract float DetectInput();

    public void InputRegisterInteractions()
    {
        inputUp = InputUp();
        inputPressed = InputPressed();
        inputDown = InputDown();
    }
    public bool InputDown()
    {
        if(DetectInput() > 0 && !inputDown) 
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    public bool InputPressed()
    {  
        if(DetectInput() > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool InputUp()
    {
        bool tmp = false;
        if(inputPressedLastFrame == true && DetectInput() <= 0)
            tmp = true;  
        inputPressedLastFrame = InputPressed();
        return tmp;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InputHandler : MonoBehaviour
{
    public Controls controls;

    public bool buttonDown;
    private void Awake()
    {
        controls = new Controls();
    }
    private void OnEnable() 
    {
        controls.Enable();
    }
    public void Start() 
    {
    }

    public int GetNumericalInput()
    {
        if(ButtonDown("one")) return 0;
        else if(ButtonDown("two")) return 1;
        else if(ButtonDown("three")) return 2;
        else if(ButtonDown("four")) return 3;
        else if(ButtonDown("five")) return 4;
        else if(ButtonDown("six")) return 5;
        else if(ButtonDown("seven")) return 6;
        else if(ButtonDown("eight")) return 7;
        else if(ButtonDown("nine")) return 8;
        else return -1;
    }

    public bool ButtonDown(string button)
    {
        if(controls.FindAction(button).ReadValue<float>() > 0) 
        {
            if(!buttonDown)
            {
                buttonDown = true;
                return true;
            }
            else return false;
        }
        else 
        {
            buttonDown = false;
            return false;
        }
    }
    public bool ButtonPressed(string button)
    {
        if(controls.FindAction(button).ReadValue<float>() > 0)
        {
            buttonDown = true;
            return true;
        }
        else 
        {
            buttonDown = false;
            return false;
        }
    }
}
 

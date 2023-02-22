using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
public class InputHandler : MonoBehaviour
{
    public Controls controls;
    private void Awake()
    {
        controls = new Controls();
    }
    private void OnEnable() 
    {
        controls.Enable();
    }
    private void OnDisable() 
    {
        controls.Disable();    
    }
    
    private void Update() {

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
        if(controls.FindAction(button).WasPressedThisFrame()) return true;
        else return false;
    }
    public bool ButtonHeld(string button)
    {
        if(controls.FindAction(button).ReadValue<float>() > 0) return true;
        else return false; 
    }
}
 

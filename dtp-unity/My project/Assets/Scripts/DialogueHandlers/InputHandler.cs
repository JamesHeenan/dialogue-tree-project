using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InputHandler : MonoBehaviour
{
    public Controls controls;

    bool continueDown;
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
        if(controls.FindAction("one").ReadValue<float>() > 0) return 1;
        else if(controls.FindAction("two").ReadValue<float>() > 0) return 2;
        else if(controls.FindAction("three").ReadValue<float>() > 0) return 3;
        else if(controls.FindAction("four").ReadValue<float>() > 0) return 4;
        else if(controls.FindAction("five").ReadValue<float>() > 0) return 5;
        else if(controls.FindAction("six").ReadValue<float>() > 0) return 6;
        else if(controls.FindAction("seven").ReadValue<float>() > 0) return 7;
        else if(controls.FindAction("eight").ReadValue<float>() > 0) return 8;
        else if(controls.FindAction("nine").ReadValue<float>() > 0) return 9;
        else return 0;
    }

        public bool ContinueDown()
    {
        if(controls.FindAction("continue").ReadValue<float>() > 0 && !continueDown) 
        {
            continueDown = true;
            return true;
        }
        else 
        {
            continueDown = false;
            return false;
        }
    }
}
 

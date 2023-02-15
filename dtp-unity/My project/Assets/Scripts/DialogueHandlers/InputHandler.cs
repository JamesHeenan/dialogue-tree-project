using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InputHandler : MonoBehaviour
{
    public Controls controls;
    public ContinueButton continueButton;
    public OptionOne one;
    public OptionTwo two;
    public OptionThree three;
    public OptionFour four;
    public OptionFive five;
    public OptionSix six;
    public OptionSeven seven;
    public OptionEight eight;
    public OptionNine nine;

    public int GetNumericalInput()
    {
        if(one.InputDown()) return 1;
        else if(two.InputDown()) return 2;
        else if(three.InputDown()) return 3;
        else if(four.InputDown()) return 4;
        else if(five.InputDown()) return 5;
        else if(six.InputDown()) return 6;
        else if(seven.InputDown()) return 7;
        else if(eight.InputDown()) return 8;
        else if(nine.InputDown()) return 9;
        else return 0;
    }
    public void Start() 
    {
        controls = new Controls();
        controls.Enable();
        continueButton.SetControls(controls);
        one.SetControls(controls);
        two.SetControls(controls);
        three.SetControls(controls);
        four.SetControls(controls);
        five.SetControls(controls);
        six.SetControls(controls);
        seven.SetControls(controls);
        eight.SetControls(controls);
        nine.SetControls(controls);

    }
}
public class ContinueButton : InputObject
{
    public override float DetectInput()
    {
        return controls.FindAction("Continue").ReadValue<float>();
    }
}   

public class OptionOne : InputObject
{
    public override float DetectInput()
    {
        return controls.FindAction("one").ReadValue<float>();
    }
} 
public class OptionTwo : InputObject
{
    public override float DetectInput()
    {
        return controls.FindAction("two").ReadValue<float>();
    }
}

public class OptionThree : InputObject
{
    public override float DetectInput()
    {
        return controls.FindAction("three").ReadValue<float>();
    }
}

public class OptionFour : InputObject
{
    public override float DetectInput()
    {
        return controls.FindAction("four").ReadValue<float>();
    }
}

public class OptionFive : InputObject
{
    public override float DetectInput()
    {
        return controls.FindAction("five").ReadValue<float>();
    }
}

public class OptionSix : InputObject
{
    public override float DetectInput()
    {
        return controls.FindAction("six").ReadValue<float>();
    }
}

public class OptionSeven : InputObject
{
    public override float DetectInput()
    {
        return controls.FindAction("seven").ReadValue<float>();
    }
}

public class OptionEight : InputObject
{
    public override float DetectInput()
    {
        return controls.FindAction("eight").ReadValue<float>();
    }
}

public class OptionNine : InputObject
{
    public override float DetectInput()
    {
        return controls.FindAction("nine").ReadValue<float>();
    }
}
 

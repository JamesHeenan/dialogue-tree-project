using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InputHandler : MonoBehaviour
{
    public ContinueButton continueButton;
    public OptionOne one;
    public OptionOne two;
    public OptionOne three;
    public OptionOne four;
    public OptionOne five;
    public OptionOne six;
    public OptionOne seven;
    public OptionOne eight;
    public OptionOne nine;

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

}
public class ContinueButton : InputObject
{
    public override float DetectInput()
    {
        throw new System.NotImplementedException();
    }
}   

public class OptionOne : InputObject
{
    public override float DetectInput()
    {
        throw new System.NotImplementedException();
    }
} 
public class OptionTwo : InputObject
{
    public override float DetectInput()
    {
        throw new System.NotImplementedException();
    }
}

public class OptionThree : InputObject
{
    public override float DetectInput()
    {
        throw new System.NotImplementedException();
    }
}

public class OptionFour : InputObject
{
    public override float DetectInput()
    {
        throw new System.NotImplementedException();
    }
}

public class OptionFive : InputObject
{
    public override float DetectInput()
    {
        throw new System.NotImplementedException();
    }
}

public class OptionSix : InputObject
{
    public override float DetectInput()
    {
        throw new System.NotImplementedException();
    }
}

public class OptionSeven : InputObject
{
    public override float DetectInput()
    {
        throw new System.NotImplementedException();
    }
}

public class OptionEight : InputObject
{
    public override float DetectInput()
    {
        throw new System.NotImplementedException();
    }
}

public class OptionNine : InputObject
{
    public override float DetectInput()
    {
        throw new System.NotImplementedException();
    }
}
 

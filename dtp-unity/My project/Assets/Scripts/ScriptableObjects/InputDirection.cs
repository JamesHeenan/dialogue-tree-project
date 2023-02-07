using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputDirection", menuName = "DialogueObjects/InputDirection", order = 0)]
public class InputDirection : Direction
{
    [TextArea(3,10)] public string[] Inputs;
}

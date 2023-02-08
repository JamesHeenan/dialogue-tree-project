using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Script
{
    public string ScriptName;
    public DirectionBase[] Directions;
}

[System.Serializable]
[CreateAssetMenu(fileName = "Dialogue", menuName = "DialogueObjects/Dialogue", order = 0)]
public class Dialogue : ScriptableObject
{
    public Script[] DialogueTree;
    
}

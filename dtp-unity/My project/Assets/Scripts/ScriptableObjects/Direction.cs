using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class DirectionBase :ScriptableObject
{
    public Location Location;
    public ActiveCharacter[] Characters;
    [TextArea(3,10)] public string Text;

    int[][] IndexOutputs;

    public ActiveCharacter GetTalking()
    {
        foreach(ActiveCharacter character in Characters)
        {
            if(character.Talking)
                return character;
        }
        return null;
    }
    public int[][] GetIndexOutputs()
    {
        return IndexOutputs;
    }

}

[CreateAssetMenu(fileName = "Direction", menuName = "DialogueObjects/Direction", order = 0)]
[System.Serializable]
public class Direction : DirectionBase
{
}

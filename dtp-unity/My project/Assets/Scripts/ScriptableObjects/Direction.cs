using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DirectionBase :ScriptableObject
{
    public Location Location;
    public ActiveCharacter[] Characters;
    public int TalkingPos;
    [TextArea(3,10)] public string Text;

    public int[][] IndexOutputs;

    public void SetTalkingPos()
    {
        for (int i = 0; i < Characters.Length; i++)
        {
            if(Characters[i].Talking)
                TalkingPos = i;
        }
    }
    public int[][] GetIndexOutputs()
    {
        return IndexOutputs;
    }

}

[CreateAssetMenu(fileName = "Direction", menuName = "DialogueObjects/Direction", order = 0)]
public class Direction : DirectionBase
{
}

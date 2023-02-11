using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActiveCharacter
{
    public string Name;
    public Character Character;
    public Emotion Emotion;
    public bool Talking;
    public Vector2 Position;
    public Sprite GetEmotion()
    {    
        if(Character.Sprites[(int)Emotion] == null)
        {
            Debug.Log("No Sprite Found for " + Name + " " + Emotion);
            return null;
        }
        else return Character.Sprites[(int)Emotion];
    } 
}

public abstract class DirectionBase :ScriptableObject
{
    public Location Location;
    public ActiveCharacter[] Characters;
    [TextArea(3,10)] public string Text;

}

[CreateAssetMenu(fileName = "Direction", menuName = "DialogueObjects/Direction", order = 0)]
[System.Serializable]
public class Direction : DirectionBase
{
    public int output = -1;
}

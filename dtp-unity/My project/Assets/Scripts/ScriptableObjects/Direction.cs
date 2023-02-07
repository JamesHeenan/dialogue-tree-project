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

[CreateAssetMenu(fileName = "Direction", menuName = "DialogueObjects/Direction", order = 0)]
public class Direction : ScriptableObject 
{
    public Location Location;
    public ActiveCharacter[] Characters;
    [TextArea(3,10)] public string Text;
}

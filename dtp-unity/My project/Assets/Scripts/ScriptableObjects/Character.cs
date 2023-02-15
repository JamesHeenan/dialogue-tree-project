using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum Emotion
{
    Default,
    Happy,
    Sad,

}
[CreateAssetMenu(fileName = "Character", menuName = "DialogueObjects/Character", order = 0)]
public class Character : ScriptableObject 
{
    public Sprite[] Sprites;
    public AudioClip SoundFont;
    public float TalkSpeed;
}

[System.Serializable]
public struct ActiveCharacter
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
    public string GetName()
    {
        return Name;
    }
}

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

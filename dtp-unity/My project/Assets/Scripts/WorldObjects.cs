using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Character
{
    public string Name;
    //Char SpriteSheet
}
public enum Emotion
{
    Default,
    Happy,
    Sad,

}
[System.Serializable]
public class Location
{
    public string Name;
    //Location Sprite    
}

[System.Serializable]
public class SceneObject
{
    public Emotion emotion;
    [TextArea(3,10)] public string sentence;
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneDirection", menuName = "DialogueObjects/SceneDirection", order = 0)]
public class SceneDirection : Direction
{
    public Location Location;
    public ActiveCharacter[] Characters;
    public ActiveCharacter GetTalking()
    {
        ActiveCharacter tmp = new ActiveCharacter();
        for (int i = 0; i < Characters.Length; i++)
        {
            if(Characters[i].Talking)
            {
                tmp = Characters[i];
                return tmp;
            }
        }
        return tmp;
    }
}
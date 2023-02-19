using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Index
{
    [SerializeField] int script;
    [SerializeField] int direction;

    public int GetScript() {return script;}
    public int GetDirection() {return direction;}
    public void SetIndex(int s, int d)
    {
        script = s;
        direction = d;
    }
    public Index ReturnDefaultIndex()
    {
        Index tmp = new Index();
        tmp.SetIndex(-1,0);
        return tmp;
    }
}
[System.Serializable]
public abstract class DirectionBase :ScriptableObject
{
    public Location Location;
    public ActiveCharacter[] Characters;
    [SerializeField] Index[] IndexOutputs = {new Index().ReturnDefaultIndex()};

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
    public Index[] GetIndexOutputs()
    {
        return IndexOutputs;
    }

}

public abstract class DirectionTextBase : DirectionBase
{
    [TextArea(3,10)] public string Text;
}

[CreateAssetMenu(fileName = "Direction", menuName = "DialogueObjects/Direction", order = 0)]
public class Direction : DirectionTextBase
{
}

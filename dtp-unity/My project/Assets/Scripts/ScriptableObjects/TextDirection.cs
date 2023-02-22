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
public abstract class Direction :ScriptableObject
{
    [SerializeField] Index[] IndexOutputs = {new Index().ReturnDefaultIndex()};

    public Index[] GetIndexOutputs()
    {
        return IndexOutputs;
    }

}

[CreateAssetMenu(fileName = "TextDirection", menuName = "DialogueObjects/TextDirection", order = 0)]
public class TextDirection : Direction
{
    [TextArea(3,10)] public string Text;
}
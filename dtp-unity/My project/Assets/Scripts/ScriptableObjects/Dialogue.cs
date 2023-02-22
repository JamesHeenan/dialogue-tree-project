using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Script
{
    public string ScriptName;
    public Direction[] Directions;
}


[CreateAssetMenu(fileName = "Dialogue", menuName = "DialogueObjects/Dialogue", order = 0)]
public class Dialogue : ScriptableObject
{
    public Script[] DialogueTree;
    public Index index;

    public Direction CurrentDirection;
    bool DirectionActive = false;

    public void ResetDialogue()
    {
        index = new Index();
        index.SetIndex(0,0);
        DirectionActive = false;
        SetCurrentDirectionFromIndex();
    }

    public void SetIndex(Index newIndex)
    {
        if(newIndex.GetScript() < DialogueTree.Length && newIndex.GetDirection() < DialogueTree[0].Directions.Length)
            index.SetIndex(newIndex.GetScript(), newIndex.GetDirection());
        else
        {
            Debug.Log("index of " + newIndex + " does not exist in the Dialogue Tree");
            throw new System.NotSupportedException();
        }
    }
    public bool GetDirectionActive()
    {
        return DirectionActive;
    }
    public void SetDirectionActive(bool input)
    {
        DirectionActive = input;
    }
    public bool CurrentDirectionMatchesIndex()
    {
        return DialogueTree[index.GetScript()].Directions[index.GetDirection()] == CurrentDirection;
    }
    public void SetCurrentDirectionFromIndex()
    {
        CurrentDirection = DialogueTree[index.GetScript()].Directions[index.GetDirection()];
    }
    public void SetIndexToNextAvailableDirection()
    {
        if(index.GetDirection() < DialogueTree[index.GetScript()].Directions.Length -1)
            index.SetIndex(index.GetScript(), index.GetDirection() + 1);
        else
            index.SetIndex(index.GetScript() + 1, index.GetDirection());
    }
    
}

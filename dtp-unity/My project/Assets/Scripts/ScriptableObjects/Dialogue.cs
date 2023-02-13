using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Script
{
    public string ScriptName;
    public DirectionBase[] Directions;
}

[System.Serializable]
[CreateAssetMenu(fileName = "Dialogue", menuName = "DialogueObjects/Dialogue", order = 0)]
public class Dialogue : ScriptableObject
{
    public Script[] DialogueTree;
    int[] index;

    DirectionBase CurrentDirection;
    bool DirectionActive = false;

    public void ResetDialogue()
    {
        index = new int[] {0,0};
        CurrentDirection = null;
        DirectionActive = false;
    }

    public void SetIndex(int[] newIndex)
    {
        if(newIndex[0] < DialogueTree.Length && newIndex[1] < DialogueTree[0].Directions.Length)
            index = newIndex;
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
        return DialogueTree[index[0]].Directions[index[1]] == CurrentDirection;
    }
    public DirectionBase GetCurrentDirection()
    {  
        return CurrentDirection;
    }
    public void SetCurrentDirectionFromIndex()
    {
        CurrentDirection = DialogueTree[index[0]].Directions[index[1]];
    }
    public void SetCurrentDirectionToNull()
    {
        CurrentDirection = null;
    }
    public void SetIndexToNextAvailableDirection()
    {
        if(index[1] < DialogueTree[index[0]].Directions.Length -1)
            index[1]++;
        else
            index[0]++;
    }
    
}

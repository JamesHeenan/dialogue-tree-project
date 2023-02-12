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
    [SerializeField] Script[] DialogueTree;
    int[] index;

    int[] outputs;

    DirectionBase CurrentDirection;

    public void ResetDialogue()
    {
        index = new int[] {0,0};
        outputs = null;
        CurrentDirection = null;
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
    public bool CurrentDirectionMatchesIndex()
    {
        return DialogueTree[index[0]].Directions[index[1]] == CurrentDirection;
    }
    public DirectionBase GetCurrentDirection()
    {  
        return CurrentDirection;
    }
    public void SetCurrentDirection()
    {
        CurrentDirection = DialogueTree[index[0]].Directions[index[1]];
    }
    public void SetIndexToNextAvailableDirection()
    {
        if(index[1] < DialogueTree[index[0]].Directions.Length -1)
            index[1]++;
        else
            index[0]++;
    }
    
}

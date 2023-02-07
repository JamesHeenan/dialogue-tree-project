using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Script
{
    public string ScriptName;
    [SerializeField] public Direction[] Directions;
}

[CreateAssetMenu(fileName = "Dialogue", menuName = "DialogueObjects/Dialogue", order = 0)]
public class Dialogue : ScriptableObject
{
    public Script[] DialogueTree;
    public Queue<Direction> EnqueueScripts()
    {
        Queue<Direction> queue = new Queue<Direction>();
        for (int i = 0; i < DialogueTree.Length; i++)
        {
            for (int j = 0; j < DialogueTree[i].Directions.Length; j++)
            {
                queue.Enqueue(DialogueTree[i].Directions[j]);               
            }

        }
        return queue;
    }

}

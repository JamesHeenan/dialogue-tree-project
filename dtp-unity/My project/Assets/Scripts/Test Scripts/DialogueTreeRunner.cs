using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTreeRunner : MonoBehaviour
{
    BehaivourTree tree;
    // Start is called before the first frame update
    void Start()
    {
        tree = ScriptableObject.CreateInstance<BehaivourTree>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

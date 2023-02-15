using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : DialogueHandler
{
    public Dialogue dialogue;
    // Start is called before the first frame update
    void Start()
    {
        StartDialogue(dialogue);
    }

    // Update is called once per frame
    void Update()
    {
        ContinueDialogue();
    }
}

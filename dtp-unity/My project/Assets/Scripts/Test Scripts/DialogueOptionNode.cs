using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueOptionNode : DialogueNode
{


    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}

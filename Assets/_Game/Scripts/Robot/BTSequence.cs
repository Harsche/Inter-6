using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSequence : BTNode
{
    public List<BTNode> children = new List<BTNode>();

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        status = Status.RUNNING;

        Print();

        foreach(BTNode child in children)
        {
            yield return bt.StartCoroutine(child.Run(bt));
            if (child.status == Status.FAILURE)
            {
                status = Status.FAILURE;
                break;
            }
        }

        if (status == Status.RUNNING) status = Status.SUCCESS;
        Print();
    }

}

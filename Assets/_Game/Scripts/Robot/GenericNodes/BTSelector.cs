using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSelector : BTNode
{
    public List<BTNode> children = new List<BTNode>();

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        status = Status.RUNNING;

        Print();

        foreach(BTNode child in children)
        {
            yield return bt.StartCoroutine(child.Run(bt));
            if (child.status == Status.SUCCESS)
            {
                status = Status.SUCCESS;
                break;
            }
        }

        if (status == Status.RUNNING) status = Status.FAILURE;
        Print();
    }

}

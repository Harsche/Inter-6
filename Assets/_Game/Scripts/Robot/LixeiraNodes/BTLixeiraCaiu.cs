using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTLixeiraCaiu : BTNode
{
    private bool lixeiraCaiu;

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        status = Status.RUNNING;
        Print();

        if (bt.npcRef.lixeiras[bt.npcRef.lixeiraIndex].caiu)
        {
            lixeiraCaiu = true;
        }
        else lixeiraCaiu = false;

        if(lixeiraCaiu)
        {
            status = Status.SUCCESS;
            Print();
        }

        if (status == Status.RUNNING) status = Status.FAILURE;
        Print();

        yield break;
    }
}

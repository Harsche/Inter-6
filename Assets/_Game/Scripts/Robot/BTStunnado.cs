using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTStunnado : BTNode
{
    private Animator npcAnimator;

    private float tempo = 8f;

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        status = Status.RUNNING;
        Print();

        npcAnimator = bt.GetComponent<Animator>();

        if (KickCheck.kickedEnemy)
        {
            npcAnimator.SetBool("isStunning", true);

            status = Status.SUCCESS;
            Print();

            tempo -= Time.deltaTime;

            if (tempo <= 0)
            {
                npcAnimator.SetBool("isStunning", false);
                KickCheck.kickedEnemy = false;
            }
        }
        else npcAnimator.SetBool("isStunning", false);

        if (status == Status.RUNNING) status = Status.FAILURE;
        Print();

        yield break;
    }


}

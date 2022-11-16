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

        if (bt.npcRef.kickRef.kickedEnemy == true)
        {
            npcAnimator.SetBool("isStunning", true);

            tempo -= Time.deltaTime;

            if (tempo <= 0)
            {
                npcAnimator.SetBool("isStunning", false);
                bt.npcRef.kickRef.kickedEnemy = false;

                tempo = 8f;
            }

            yield return null;

            status = Status.SUCCESS;
            Print();
        }
        else npcAnimator.SetBool("isStunning", false);

        if (status == Status.RUNNING) status = Status.FAILURE;
        Print();
    }


}

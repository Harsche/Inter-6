using System.Collections;
using UnityEngine;

public class BTStunnado : BTNode
{
    private float tempo = 8f;
    private static readonly int IsStunning = Animator.StringToHash("isStunning");

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        status = Status.RUNNING;
        Print();
        

        if (bt.npcRef.kickRef.kickedEnemy == true)
        {
            bt.iaAnimator.SetBool(IsStunning, true);

            tempo -= Time.deltaTime;

            if (tempo <= 0)
            {
                bt.iaAnimator.SetBool(IsStunning, false);
                bt.npcRef.kickRef.kickedEnemy = false;

                tempo = 8f;
            }

            yield return null;

            status = Status.SUCCESS;
            Print();
        }
        else bt.iaAnimator.SetBool(IsStunning, false);

        if (status == Status.RUNNING) status = Status.FAILURE;
        Print();
    }


}

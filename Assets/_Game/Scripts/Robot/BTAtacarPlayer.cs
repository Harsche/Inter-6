using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAtacarPlayer : BTNode
{
    private GameObject alvo = GameObject.FindGameObjectWithTag("Player");
    private Animator npcAnimator;

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        status = Status.RUNNING;
        Print();

        npcAnimator = bt.GetComponent<Animator>();
        PlayerMovement playerRef = alvo.GetComponent<PlayerMovement>();

        while (Vector3.Distance(alvo.transform.position, bt.transform.position) < 0.7f && playerRef.isDead == false)
        {
            npcAnimator.SetBool("isAttacking", true);
            status = Status.SUCCESS;

            if(Vector3.Distance(alvo.transform.position, bt.transform.position) > 0.7f)
            {
                npcAnimator.SetBool("isAtttacking", false);
                break;
            }

            yield return null;
        }
        if (status == Status.RUNNING) status = Status.FAILURE;
    }
}

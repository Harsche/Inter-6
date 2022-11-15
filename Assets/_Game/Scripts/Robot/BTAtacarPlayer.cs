using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAtacarPlayer : BTNode
{
    private GameObject alvo = GameObject.FindGameObjectWithTag("Player");
    private Animator npcAnimator;

    private float layerWeightTrue = 1f;
    private float layerWeightFalse = 0f;

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        status = Status.RUNNING;
        Print();

        npcAnimator = bt.GetComponent<Animator>();
        PlayerMovement playerRef = alvo.GetComponent<PlayerMovement>();

        while (Vector3.Distance(alvo.transform.position, bt.transform.position) <= 1f && playerRef.isDead == false)
        {
            npcAnimator.SetBool("isAttacking", true);
            status = Status.SUCCESS;

            if (Vector3.Distance(alvo.transform.position, bt.transform.position) > 1f && Vector3.Distance(alvo.transform.position, bt.transform.position) < 2f)
            {
                npcAnimator.SetLayerWeight(npcAnimator.GetLayerIndex("MovingAttack"), layerWeightTrue);
            }
            else if(Vector3.Distance(alvo.transform.position, bt.transform.position) > 2f)
            {
                npcAnimator.SetBool("isAtttacking", false);
                npcAnimator.SetLayerWeight(npcAnimator.GetLayerIndex("MovingAttack"), layerWeightFalse);
                break;
            }

            yield return null;
        }

        if (status == Status.RUNNING) status = Status.FAILURE;
        Print();
    }
}

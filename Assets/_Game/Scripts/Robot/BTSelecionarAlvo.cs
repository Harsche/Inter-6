using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTSelecionarAlvo : BTNode
{
    private GameObject player = GameObject.FindGameObjectWithTag("Player");
    private Animator npcAnimator;

    private bool patrulhar;

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        status = Status.RUNNING;
        Print();

        npcAnimator = bt.GetComponent<Animator>();

        if (Vector3.Distance(player.transform.position, bt.transform.position) > 4f)
        {
            patrulhar = true;
        }

        if (Vector3.Distance(player.transform.position, bt.transform.position) < 4f)
        {
            patrulhar = false;
            npcAnimator.SetBool("isWalking", false);
        }

        while (patrulhar)
        {
            if (bt.npcRef.pointIndex == bt.npcRef.npcPoints.Count)
            {
                bt.npcRef.pointIndex = 0;
                npcAnimator.SetBool("isWalking", false);
            }
            else if (bt.npcRef.pointIndex < bt.npcRef.npcPoints.Count && bt.transform.position.x == bt.npcRef.npcPoints[bt.npcRef.pointIndex].position.x) bt.npcRef.pointIndex++;

            status = Status.SUCCESS;
            Print();

            break;
        }

        if (status == Status.RUNNING) status = Status.FAILURE;
        Print();

        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTSelecionarAlvo : BTNode
{
    private GameObject player = Player.Instance.gameObject;
    private bool patrulhar;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        status = Status.RUNNING;
        Print();

        if (Vector3.Distance(player.transform.position, bt.transform.position) > 5f)
        {
            patrulhar = true;
        }

        if (Vector3.Distance(player.transform.position, bt.transform.position) < 5f)
        {
            patrulhar = false;
            bt.iaAnimator.SetBool(IsWalking, false);
        }

        while (patrulhar)
        {
            if (bt.npcRef.pointIndex >= bt.npcRef.npcPoints.Count - 1f)
            {
                bt.npcRef.pointIndex = 0;
                bt.iaAnimator.SetBool(IsWalking, false);
            }
            else if (bt.npcRef.pointIndex <= bt.npcRef.npcPoints.Count && bt.iaNavMeshAgent.remainingDistance <= bt.iaNavMeshAgent.stoppingDistance) bt.npcRef.pointIndex++;

            status = Status.SUCCESS;
            Print();

            break;
        }

        if (status == Status.RUNNING) status = Status.FAILURE;
        Print();

        yield return null;
    }
}

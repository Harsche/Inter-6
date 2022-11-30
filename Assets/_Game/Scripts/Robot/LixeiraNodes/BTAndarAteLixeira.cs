using System.Collections;
using UnityEngine;

public class BTAndarAteLixeira : BTNode
{
    private bool verificar;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        status = Status.RUNNING;
        Print();
        
        if (Vector3.Distance(bt.transform.position, bt.npcRef.lixeiras[bt.npcRef.lixeiraIndex].transform.position) > 2f && bt.npcRef.lixeiras[bt.npcRef.lixeiraIndex].caiu)
        {
            verificar = true;
        }
        else verificar = false;

        while (verificar)
        {
            bt.npcRef.olhoDir.material = bt.npcRef.patrulhaOlho;
            bt.npcRef.olhoEsq.material = bt.npcRef.patrulhaOlho;

            bt.IaNavMeshAgent.angularSpeed = 550;

            bt.IaNavMeshAgent.SetDestination(bt.npcRef.lixeiras[bt.npcRef.lixeiraIndex].transform.position);
            bt.IaAnimator.SetBool(IsWalking, true);

            status = Status.SUCCESS;
            Print();

            break;
        }

        if (status == Status.RUNNING) status = Status.FAILURE;
        Print();

        yield return null;
    }
}

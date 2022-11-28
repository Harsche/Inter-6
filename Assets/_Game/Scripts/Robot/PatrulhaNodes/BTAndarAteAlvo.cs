using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTAndarAteAlvo : BTNode
{
    private GameObject player = GameObject.FindGameObjectWithTag("Player");
    private Animator npcAnimator;
    private NavMeshAgent naveM;

    private bool patrulhar = false;

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        status = Status.RUNNING;
        Print();

        npcAnimator = bt.GetComponent<Animator>();
        naveM = bt.GetComponent<NavMeshAgent>();

        if (Vector3.Distance(player.transform.position, bt.transform.position) > 5f || (bt.npcRef.lixeiras.Count > 0 && bt.npcRef.lixeiras[bt.npcRef.lixeiraIndex].caiu == false))
        {
            patrulhar = true;
        }

        if (Vector3.Distance(player.transform.position, bt.transform.position) < 5f ||  bt.npcRef.lixeiras.Count <= 0 || bt.npcRef.lixeiras[bt.npcRef.lixeiraIndex].caiu)
        {
            patrulhar = false;
            npcAnimator.SetBool("isWalking", false);
        }

        while (patrulhar){
            naveM.SetDestination(bt.npcRef.npcPoints[bt.npcRef.pointIndex].position);
            npcAnimator.SetBool("isWalking", true);

            status = Status.SUCCESS;
            Print();

            break;
        }

        if (status == Status.RUNNING) status = Status.FAILURE;
        Print();

        yield return null;
    }
}

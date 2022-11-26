using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTOlharAlvo : BTNode
{
    private GameObject player = GameObject.FindGameObjectWithTag("Player");
    private Animator npcAnimator;
    private NavMeshAgent naveM;

    private bool patrulhar;

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        status = Status.RUNNING;
        Print();

        naveM = bt.GetComponent<NavMeshAgent>();

        Vector3 lookRef = bt.npcRef.npcPoints[bt.npcRef.pointIndex].position;

        if (Vector3.Distance(player.transform.position, bt.transform.position) > 5f)
        {
            patrulhar = true;
        }

        if (Vector3.Distance(player.transform.position, bt.transform.position) < 5f)
        {
            patrulhar = false;
            npcAnimator.SetBool("isWalking", false);
        }

        if(patrulhar)
        {
            naveM.transform.LookAt(lookRef);
            //naveM.transform.rotation = Quaternion.Euler(0, bt.transform.rotation.y, 0);
            status = Status.SUCCESS;
            Print();
        }

        if (status == Status.RUNNING) status = Status.FAILURE;
        Print();

        yield break;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTVerificandoLixeira : BTNode
{
    private Animator npcAnimator;
    private NavMeshAgent naveM;

    private bool verificar;

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        status = Status.RUNNING;
        Print();

        npcAnimator = bt.GetComponent<Animator>();
        naveM = bt.GetComponent<NavMeshAgent>();

        if (Vector3.Distance(bt.transform.position, bt.npcRef.lixeiras[bt.npcRef.lixeiraIndex].transform.position) <= 2f && bt.npcRef.lixeiras[bt.npcRef.lixeiraIndex].caiu)
        {
            verificar = true;
        }
        else verificar = false;

        while (verificar)
        {
            if (bt.npcRef.lixeiras[bt.npcRef.lixeiraIndex].timer <= 0f) break;

            npcAnimator.SetBool("isWalking", false);

            bt.transform.position = bt.transform.position;

            status = Status.SUCCESS;
            Print();

            break;
        }

        if (status == Status.RUNNING) status = Status.FAILURE;
        Print();

        yield return null;
    }
}

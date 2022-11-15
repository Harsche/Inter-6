using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTPatrulhaFinal : BTNode
{
    private GameObject alvo2 = GameObject.FindGameObjectWithTag("PatrulhaFinal");
    private GameObject player = GameObject.FindGameObjectWithTag("Player");
    private Animator npcAnimator;
    private NavMeshAgent naveM;

    private bool patrulhar;
    public static bool VerificaAlvo2 = false;

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        status = Status.RUNNING;
        Print();

        npcAnimator = bt.GetComponent<Animator>();
        naveM = bt.GetComponent<NavMeshAgent>();

        if (Vector3.Distance(player.transform.position, bt.transform.position) > 4f)
        {
            patrulhar = true;
        }

        if (patrulhar)
        {
            if (VerificaAlvo2 == false)
            {
                bt.transform.LookAt(alvo2.transform.position);

                naveM.SetDestination(alvo2.transform.position);

                npcAnimator.SetBool("isWalking", true);
            }
        }

        if (bt.transform.position.x == alvo2.transform.position.x)
        {
            VerificaAlvo2 = true;

            BTPatrulhaInicial.VerificaAlvo1 = false;

            npcAnimator.SetBool("isWalking", false);

            status = Status.SUCCESS;
            Print();

            patrulhar = false;
        }

        if (Vector3.Distance(player.transform.position, bt.transform.position) < 4f)
        {
            patrulhar = false;
            npcAnimator.SetBool("isWalking", false);
        }

        if (status == Status.RUNNING) status = Status.FAILURE;
        Print();

        yield return null;
    }
}

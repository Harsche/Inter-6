using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTPatrulhaInicial : BTNode
{
    private GameObject alvo1 = GameObject.FindGameObjectWithTag("PatrulhaInicial");
    private GameObject player = GameObject.FindGameObjectWithTag("Player");
    private Animator npcAnimator;
    private NavMeshAgent naveM;

    private bool patrulhar;
    public static bool VerificaAlvo1 = false;

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
            if (VerificaAlvo1 == false)
            {
                bt.transform.LookAt(alvo1.transform.position);

                naveM.SetDestination(alvo1.transform.position);

                npcAnimator.SetBool("isWalking", true);
            }
        }

        if (bt.transform.position.x == alvo1.transform.position.x)
        {
            VerificaAlvo1 = true;
            BTPatrulhaFinal.VerificaAlvo2 = false;

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

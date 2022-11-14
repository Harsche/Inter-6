using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTPatrulha : BTNode
{
    private GameObject alvo1 = GameObject.FindGameObjectWithTag("PatrulhaInicial");
    private GameObject alvo2 = GameObject.FindGameObjectWithTag("PatrulhaFinal");
    private GameObject player = GameObject.FindGameObjectWithTag("Player");
    private Animator npcAnimator;
    private NavMeshAgent naveM;

    private bool patrulhar;
    private bool VerificaAlvo1 = false;
    private bool VerificaAlvo2 = false;

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
            if (VerificaAlvo1 == false && bt.transform.position != alvo1.transform.position)
            {
                bt.transform.LookAt(alvo1.transform.position);

                naveM.SetDestination(alvo1.transform.position);

                npcAnimator.SetBool("isWalking", true);

                VerificaAlvo1 = true;
                VerificaAlvo2 = false;
            }
            else if (VerificaAlvo1 == true && VerificaAlvo2 == false && bt.transform.position != alvo2.transform.position)
            {
                bt.transform.LookAt(alvo2.transform.position);

                naveM.SetDestination(alvo2.transform.position);

                VerificaAlvo1 = false;
                VerificaAlvo2 = true;
                status = Status.SUCCESS;
                Print();
                yield return null;
            }

            if (Vector3.Distance(player.transform.position, bt.transform.position) < 4f)
            {
                patrulhar = false;
                npcAnimator.SetBool("isWalking", false);
                //break;
            }
        }

        if (status == Status.RUNNING) status = Status.FAILURE;
        Print();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTAndarAtePlayer : BTNode
{
    private GameObject alvo = GameObject.FindGameObjectWithTag("Player");
    private Animator npcAnimator;
    private NavMeshAgent naveM;

    [SerializeField] private float ataqueDist = 0.7f;
    [SerializeField] private float tempo = 10f;

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        status = Status.RUNNING;
        Print();

        npcAnimator = bt.GetComponent<Animator>();
        naveM = bt.GetComponent<NavMeshAgent>();

        while (Vector3.Distance(alvo.transform.position, bt.transform.position) < 5f)
        {
            bt.transform.LookAt(alvo.transform.position);

            naveM.SetDestination(alvo.transform.position);

            npcAnimator.SetBool("isRunning", true);

            if (Vector3.Distance(alvo.transform.position, bt.transform.position) < ataqueDist)
            {
                npcAnimator.SetBool("isRunning", false);
                status = Status.SUCCESS;
                break;
            }

            tempo -= Time.deltaTime;
            if (tempo < 0f) break;

            yield return null;
        }

        if(status == Status.RUNNING) status = Status.FAILURE;
        Print();
    }
}

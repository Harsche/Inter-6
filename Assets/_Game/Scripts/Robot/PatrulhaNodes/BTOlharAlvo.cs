using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTOlharAlvo : BTNode
{
    private GameObject player = GameObject.FindGameObjectWithTag("Player");
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private Animator npcAnimator;
    private NavMeshAgent naveM;

    private bool patrulhar;

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        status = Status.RUNNING;
        Print();

        naveM = bt.GetComponent<NavMeshAgent>();

        Vector3 lookRef = bt.npcRef.npcPoints[bt.npcRef.pointIndex].position;

        if (Vector3.Distance(player.transform.position, bt.transform.position) < 5f)
        {
            Ray raio = new Ray(bt.transform.position, player.transform.position);
            RaycastHit hit;

            if (Physics.Raycast(raio, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    patrulhar = false;
                    bt.IaAnimator.SetBool(IsWalking, false);
                }
            }
            else patrulhar = true;
        }
        else patrulhar = true;

        if (patrulhar)
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

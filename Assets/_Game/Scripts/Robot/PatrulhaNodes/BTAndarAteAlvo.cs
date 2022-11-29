using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTAndarAteAlvo : BTNode
{
    private GameObject player = Player.Instance.gameObject;
    private bool patrulhar = false;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        status = Status.RUNNING;
        Print();

        if (Vector3.Distance(player.transform.position, bt.transform.position) > 5f && (bt.npcRef.lixeiras.Count > 0 && bt.npcRef.lixeiras[bt.npcRef.lixeiraIndex].caiu == false))
        {
            patrulhar = true;
        }

        if (Vector3.Distance(player.transform.position, bt.transform.position) < 5f)
        {
            bt.transform.GetChild(2).LookAt(player.transform.GetChild(4).position);

            Ray raio = new(bt.transform.GetChild(2).position, bt.transform.GetChild(2).TransformDirection(Vector3.forward));

            RaycastHit hit;

            //Debug.DrawRay(bt.transform.GetChild(2).position, bt.transform.GetChild(2).TransformDirection(Vector3.forward) * 50f, Color.yellow);

            if (Physics.Raycast(raio, out hit, 50f, ~0, QueryTriggerInteraction.Ignore))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    patrulhar = false;
                    bt.iaAnimator.SetBool(IsWalking, false);
                }
                else patrulhar = true;
            }
        } else patrulhar = true;

        if (bt.npcRef.lixeiras.Count <= 0 || bt.npcRef.lixeiras[bt.npcRef.lixeiraIndex].caiu)
        {
            patrulhar = false;
            bt.iaAnimator.SetBool(IsWalking, false);
        }

        while (patrulhar)
        {
            bt.npcRef.olhoDir.material = bt.npcRef.patrulhaOlho; 
            bt.npcRef.olhoEsq.material = bt.npcRef.patrulhaOlho; 

            //bt.transform.LookAt(bt.npcRef.npcPoints[bt.npcRef.pointIndex].position);
            bt.iaNavMeshAgent.angularSpeed = 550;

            bt.iaNavMeshAgent.SetDestination(bt.npcRef.npcPoints[bt.npcRef.pointIndex].position);
            bt.iaAnimator.SetBool(IsWalking, true);

            status = Status.SUCCESS;
            Print();

            break;
        }

        if (status == Status.RUNNING) status = Status.FAILURE;
        Print();

        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTSelecionarAlvo : BTNode
{
    private GameObject player = Player.Instance.gameObject;
    private bool patrulhar;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        status = Status.RUNNING;
        Print();

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
                    bt.IaAnimator.SetBool(IsWalking, false);
                } else patrulhar = true;
            }
        } else patrulhar = true;

        if (bt.npcRef.npcPoints.Count <= 0) patrulhar = false;

        while (patrulhar)
        {
            if (bt.npcRef.pointIndex == bt.npcRef.npcPoints.Count)
            {
                bt.npcRef.pointIndex = 0;
                bt.IaAnimator.SetBool(IsWalking, false);
            }
            else if (bt.npcRef.pointIndex <= bt.npcRef.npcPoints.Count - 1 && bt.IaNavMeshAgent.remainingDistance <= bt.IaNavMeshAgent.stoppingDistance) bt.npcRef.pointIndex++;

            status = Status.SUCCESS;
            Print();

            break;
        }

        if (status == Status.RUNNING) status = Status.FAILURE;
        Print();

        yield return null;
    }
}

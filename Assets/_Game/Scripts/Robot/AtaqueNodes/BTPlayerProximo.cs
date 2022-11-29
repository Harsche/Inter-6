using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPlayerProximo : BTNode
{
    private GameObject player = Player.Instance.gameObject;

    [SerializeField] private float areaDeteccao = 5f;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        PlayerMovement playerRef = Player.Instance.PlayerMovement;

        if (playerRef.isDead)
        {
            yield break;
        }

        status = Status.RUNNING;
        Print();

        if (Vector3.Distance(player.transform.position, bt.transform.position) < areaDeteccao)
        {
            bt.npcRef.olhoDir.material = bt.npcRef.ataqueOlho;
            bt.npcRef.olhoEsq.material = bt.npcRef.ataqueOlho;

            bt.transform.GetChild(2).LookAt(player.transform.GetChild(4).position);

            Ray raio = new(bt.transform.GetChild(2).position, bt.transform.GetChild(2).TransformDirection(Vector3.forward));

            RaycastHit hit;

            //Debug.DrawRay(bt.transform.GetChild(2).position, bt.transform.GetChild(2).TransformDirection(Vector3.forward) * 50f, Color.yellow);

            if (Physics.Raycast(raio, out hit, 50f, ~0, QueryTriggerInteraction.Ignore))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    status = Status.SUCCESS;
                }
            }   
        }
        else
        {
            bt.iaAnimator.SetBool(IsRunning, false);
            bt.iaAnimator.SetBool(IsAttacking, false);
        }

        if (status == Status.RUNNING) status = Status.FAILURE;
        Print();

        yield break;
    }


}

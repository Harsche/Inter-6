using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTPlayerProximo : BTNode
{
    private GameObject player;
    private Animator npcAnimator;

    [SerializeField] private float areaDeteccao = 4f;

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        npcAnimator = bt.GetComponent<Animator>();

        status = Status.RUNNING;
        Print();

        if (Vector3.Distance(player.transform.position, bt.transform.position) < areaDeteccao)
        {
            status = Status.SUCCESS;
        }
        else
        {
            npcAnimator.SetBool("isRunning", false);
            npcAnimator.SetBool("isAttacking", false);
        }

        if (status == Status.RUNNING) status = Status.FAILURE;
        Print();

        yield break;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAndarAtePlayer : BTNode
{
    private GameObject alvo = GameObject.FindGameObjectWithTag("Player");
    private Animator npcAnimator;
    private CharacterController charController;

    [SerializeField] private Vector3 npcMovement;
    [SerializeField] private float velocity = 3.5f;
    [SerializeField] private float ataqueDist = 0.7f;
    [SerializeField] private float tempo = 10f;

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        status = Status.RUNNING;
        Print();

        npcAnimator = bt.GetComponent<Animator>();
        charController = bt.GetComponent<CharacterController>();

        while (Vector3.Distance(alvo.transform.position, bt.transform.position) < 5f)
        {
            bt.transform.LookAt(alvo.transform.position);

            npcMovement = bt.transform.forward * velocity * Time.deltaTime;
            charController.Move(npcMovement);
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

using System.Collections;
using UnityEngine;

public class BTAndarAtePlayer : BTNode
{
    private GameObject alvo = Player.Instance.gameObject;

    [SerializeField] private float tempo = 10f;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        status = Status.RUNNING;
        Print();

        PlayerMovement playerRef = Player.Instance.PlayerMovement;

        if (playerRef.isDead)
        {
            yield break;
        }

        while (Vector3.Distance(alvo.transform.position, bt.transform.position) < 5f)
        {
            bt.iaNavMeshAgent.angularSpeed = 0;
            bt.transform.LookAt(alvo.transform.position);

            bt.iaAnimator.SetBool(IsRunning, true);
            bt.iaNavMeshAgent.SetDestination(alvo.transform.position);

            if (Vector3.Distance(alvo.transform.position, bt.transform.position) <= 2.77f)
            {
                bt.iaAnimator.SetBool(IsRunning, false);
                bt.iaAnimator.SetBool(IsWalking, false);
                status = Status.SUCCESS;
                break;
            }

            if (Vector3.Distance(alvo.transform.position, bt.transform.position) > 3.5f)
            {
                bt.iaAnimator.SetBool(IsAttacking, false);
            }

            tempo -= Time.deltaTime;
            if (tempo < 0f) break;

            yield return null;
        }

        if(status == Status.RUNNING) status = Status.FAILURE;
        Print();
    }
}

using System.Collections;
using UnityEngine;

public class BTAtacarPlayer : BTNode
{
    private GameObject alvo = Player.Instance.gameObject;

    private float layerWeightTrue = 1f;
    private float layerWeightFalse = 0f;
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        status = Status.RUNNING;
        Print();

        PlayerMovement playerRef = Player.Instance.PlayerMovement;

        if (playerRef.isDead)
        {
            yield break;
        }

        bt.IaNavMeshAgent.angularSpeed = 0;
        bt.transform.LookAt(alvo.transform.position);

        if (Vector3.Distance(alvo.transform.position, bt.transform.position) <= 2.3f)
        {
            bt.IaAnimator.SetBool(IsAttacking, true);
            bt.IaAnimator.SetBool(IsWalking, false);

            if (bt.IaAnimator.GetBool(IsAttacking))
            {
                bt.gameObject.GetComponentInChildren<BoxCollider>().enabled = true;
            }
            else bt.gameObject.GetComponentInChildren<BoxCollider>().enabled = false;

            status = Status.SUCCESS;
        }
        else if (Vector3.Distance(alvo.transform.position, bt.transform.position) >= 2.3f && Vector3.Distance(alvo.transform.position, bt.transform.position) <= 3f)
        {
            bt.IaAnimator.SetBool(IsAttacking, true);
            bt.IaAnimator.SetBool(IsWalking, false);

            bt.IaAnimator.SetLayerWeight(bt.IaAnimator.GetLayerIndex("MovingAttack"), layerWeightTrue);

            if (bt.IaAnimator.GetBool(IsAttacking))
            {
                bt.gameObject.GetComponentInChildren<BoxCollider>().enabled = true;
            }
            else bt.gameObject.GetComponentInChildren<BoxCollider>().enabled = false;

            status = Status.SUCCESS;
        }
        else if (Vector3.Distance(alvo.transform.position, bt.transform.position) > 3f)
        {
            bt.gameObject.GetComponentInChildren<BoxCollider>().enabled = false;

            bt.IaAnimator.SetBool(IsAttacking, false);
            bt.IaAnimator.SetLayerWeight(bt.IaAnimator.GetLayerIndex("MovingAttack"), layerWeightFalse);
        }

        if (status == Status.RUNNING) status = Status.FAILURE;
        Print();

        yield return null;
    }
}

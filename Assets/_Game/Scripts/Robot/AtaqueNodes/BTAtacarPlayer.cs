using System.Collections;
using UnityEngine;

public class BTAtacarPlayer : BTNode
{
    private GameObject alvo = Player.Instance.gameObject;

    private float layerWeightTrue = 1f;
    private float layerWeightFalse = 0f;
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    public override IEnumerator Run(BehaviourTree2 bt)
    {
        status = Status.RUNNING;
        Print();
        
        PlayerMovement playerRef = Player.Instance.PlayerMovement;

        if(playerRef.isDead)
        {
            yield break;
        }

        if (Vector3.Distance(alvo.transform.position, bt.transform.position) <= 1.2f)
        {
            bt.iaAnimator.SetBool(IsAttacking, true);
            status = Status.SUCCESS;
        }
        else if (Vector3.Distance(alvo.transform.position, bt.transform.position) > 1.5f && Vector3.Distance(alvo.transform.position, bt.transform.position) < 2.5f)
        {
            bt.iaAnimator.SetBool(IsAttacking, true);
            bt.iaAnimator.SetLayerWeight(bt.iaAnimator.GetLayerIndex("MovingAttack"), layerWeightTrue);
            status = Status.SUCCESS;
        }
        else if (Vector3.Distance(alvo.transform.position, bt.transform.position) > 2f)
        {
            bt.iaAnimator.SetBool(IsAttacking, false);
            bt.iaAnimator.SetLayerWeight(bt.iaAnimator.GetLayerIndex("MovingAttack"), layerWeightFalse);
        }

        if (status == Status.RUNNING) status = Status.FAILURE;
        Print();

        yield return null;
    }
}

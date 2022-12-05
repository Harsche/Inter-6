using UnityEngine;

public class HeadAim : MonoBehaviour{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform target;

    private void Update(){
        animator.SetLookAtWeight(1f);
        animator.SetLookAtPosition(target.position);
    }
}
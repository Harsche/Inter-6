using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class JumpTraining : MonoBehaviour{
    [SerializeField] private Transform checkPosition;
    [SerializeField] private int jumpsRequired;
    [SerializeField] private int jumpCount;
    [SerializeField] private float cooldownTime = 0.2f;
    [SerializeField] private UnityEvent onExecuteAllJumps;
    private bool canAddJump;
    private Coroutine cooldownCoroutine;

    private void Awake(){
        canAddJump = true;
    }

    private void OnTriggerExit(Collider other){
        if (other.gameObject.name != "KickCollider") return;
        HandleJump(other.transform.position);
    }

    private void HandleJump(Vector3 collisionPosition){
        if (!canAddJump || jumpCount >= jumpsRequired || collisionPosition.y <= checkPosition.position.y) return;
        jumpCount++;
        canAddJump = false;
        if (cooldownCoroutine != null) StopCoroutine(cooldownCoroutine);
        cooldownCoroutine = StartCoroutine(CooldownCoroutine());
        if (jumpCount == jumpsRequired) onExecuteAllJumps?.Invoke();
    }

    private IEnumerator CooldownCoroutine(){
        yield return new WaitForSeconds(cooldownTime);
        canAddJump = true;
    }
}
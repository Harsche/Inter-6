using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class KickTraining : MonoBehaviour{
    [SerializeField] private int kicksRequired;
    [SerializeField] private int kickCount;
    [SerializeField] private float cooldownTime = 0.2f;
    [SerializeField] private UnityEvent onExecuteAllKicks;
    private bool canAddKick;
    private Coroutine cooldownCoroutine;

    private void Awake(){
        canAddKick = true;
    }

    private void OnCollisionEnter(Collision other){
        if (other.gameObject.name != "KickCollider") return;
        HandleKick();
    }

    private void HandleKick(){
        if (!canAddKick || kickCount >= kicksRequired) return;
        kickCount++;
        canAddKick = false;
        if (cooldownCoroutine != null) StopCoroutine(cooldownCoroutine);
        cooldownCoroutine = StartCoroutine(CooldownCoroutine());
        if (kickCount == kicksRequired) onExecuteAllKicks?.Invoke();
    }

    private IEnumerator CooldownCoroutine(){
        yield return new WaitForSeconds(cooldownTime);
        canAddKick = true;
    }
}
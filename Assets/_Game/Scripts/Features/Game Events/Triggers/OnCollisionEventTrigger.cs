using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OnCollisionEventTrigger : MonoBehaviour{
    [SerializeField] private bool isCollisionTrigger = true;
    [SerializeField] private bool triggerOnEnter;
    [SerializeField] private bool triggerOnStay;
    [SerializeField] private bool triggerOnExit;
    [SerializeField] private float delay;
    [SerializeField] private UnityEvent onTriggerEnter;

    private void OnCollisionEnter(Collision other){
        if (!triggerOnEnter || isCollisionTrigger || !other.gameObject.CompareTag("Player")) return;
        StartCoroutine(TriggerEvent());
    }

    private void OnCollisionExit(Collision other){
        if (!triggerOnExit || isCollisionTrigger || !other.gameObject.CompareTag("Player")) return;
        StartCoroutine(TriggerEvent());
    }

    private void OnCollisionStay(Collision other){
        if (!triggerOnStay || isCollisionTrigger || !other.gameObject.CompareTag("Player")) return;
        StartCoroutine(TriggerEvent());
    }

    private void OnTriggerEnter(Collider other){
        if (!triggerOnEnter || !isCollisionTrigger || !other.CompareTag("Player")) return;
        StartCoroutine(TriggerEvent());
    }

    private void OnTriggerExit(Collider other){
        if (!triggerOnExit || !isCollisionTrigger || !other.CompareTag("Player")) return;
        StartCoroutine(TriggerEvent());
    }

    private void OnTriggerStay(Collider other){
        if (!triggerOnStay || !isCollisionTrigger || !other.CompareTag("Player")) return;
        StartCoroutine(TriggerEvent());
    }

    private IEnumerator TriggerEvent(){
        yield return new WaitForSeconds(delay);
        onTriggerEnter?.Invoke();
    }
}
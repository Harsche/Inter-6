using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnterEventTrigger : MonoBehaviour{
    [SerializeField] private float delay;
    [SerializeField] private UnityEvent onTriggerEnter;
    

    private void OnTriggerEnter(Collider other){
        if (!other.CompareTag("Player")) return;
        StartCoroutine(TriggerEvent());
    }

    private IEnumerator TriggerEvent(){
        yield return new WaitForSeconds(delay);
        onTriggerEnter?.Invoke();
    }
}
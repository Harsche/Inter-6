using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class OnKeyPressedTrigger : MonoBehaviour{
    [SerializeField] private bool anyKey;
    [SerializeField] private KeyCode key;
    [SerializeField] private float delay;
    [SerializeField] private bool disableOnActivate;
    [SerializeField] private UnityEvent onKeyPress;

    private void Update(){
        if (Input.anyKeyDown && anyKey){
            if (disableOnActivate) enabled = false;
            StartCoroutine(TriggerEvent());
            return;
        }
        if(!Input.GetKeyDown(key)) return;
        if (disableOnActivate) enabled = false;
        StartCoroutine(TriggerEvent());
    }

    private IEnumerator TriggerEvent(){
        yield return new WaitForSeconds(delay);
        onKeyPress?.Invoke();
    }
}
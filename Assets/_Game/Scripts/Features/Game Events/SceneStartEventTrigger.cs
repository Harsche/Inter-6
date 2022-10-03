using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SceneStartEventTrigger : MonoBehaviour{
    [SerializeField] private float delay;
    [SerializeField] private UnityEvent onSceneStart;

    private void Start(){
        if (!enabled) return;
        StartCoroutine(TriggerEvent());
    }
    
    private IEnumerator TriggerEvent(){
        yield return new WaitForSeconds(delay);
        onSceneStart?.Invoke();
    }
}
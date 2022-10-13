using UnityEngine;
using UnityEngine.Events;

public class OnInteractEventTrigger : MonoBehaviour, IInteractable{
    [SerializeField] private bool disableOnActivate;
    [SerializeField] private UnityEvent onInteract;
    [SerializeField] private GameObject outline;

    private bool selected;

    private void Awake(){
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    public bool Interact(){
        onInteract?.Invoke();
        if (disableOnActivate) gameObject.SetActive(false);
        return true;
    }

    public void ToggleHighlight(bool active){
        outline.layer = active
            ? LayerMask.NameToLayer("Outline")
            : LayerMask.NameToLayer("Default");
        outline.SetActive(active);
    }
}
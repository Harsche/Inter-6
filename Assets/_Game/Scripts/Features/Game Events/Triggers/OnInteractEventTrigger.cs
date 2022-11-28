using UnityEngine;
using UnityEngine.Events;

public class OnInteractEventTrigger : MonoBehaviour, IInteractable{
    [SerializeField] private bool canInteract = true;
    [SerializeField] private bool disableOnActivate;
    [SerializeField] private UnityEvent onInteract;
    [SerializeField] private GameObject outline;

    private bool selected;

    private void Awake(){
        OnValidate();
    }

    private void OnValidate(){
        gameObject.layer = LayerMask.NameToLayer("Interactable");
        if (outline == null) return;
        outline.layer = LayerMask.NameToLayer("Outline");
        outline.SetActive(false);
    }

    public bool Interact(){
        if (!canInteract) return false;
        onInteract?.Invoke();
        if (disableOnActivate) canInteract = false;
        return true;
    }

    public void ToggleHighlight(bool active){
        if (!canInteract) active = false;
        outline.layer = active
            ? LayerMask.NameToLayer("Outline")
            : LayerMask.NameToLayer("Default");
        outline.SetActive(active);
    }

    public void ToggleInteraction(bool value){
        canInteract = value;
    }
}
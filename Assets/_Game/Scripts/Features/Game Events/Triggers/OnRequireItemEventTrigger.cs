using UnityEngine;
using UnityEngine.Events;
using Inventory;

public class OnRequireItemEventTrigger : MonoBehaviour, IInteractable{
    [SerializeField] private bool canInteract = true;
    [SerializeField] private bool disableObject;
    [SerializeField] private bool consumeItem = true;
    [SerializeField] private Item requiredItem;
    [SerializeField] private UnityEvent onUseItem;
    [SerializeField] private UnityEvent missingItem;
    [SerializeField] private GameObject outline;

    private bool selected;
    
    private void OnValidate(){
        gameObject.layer = LayerMask.NameToLayer("Interactable");
        if (outline == null) return;
        outline.layer = LayerMask.NameToLayer("Outline");
        outline.SetActive(false);
    }

    private void Awake(){
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    public bool Interact(){
        if (!canInteract) return false;
        bool interacted = consumeItem
            ? GameInventory.Instance.RemoveItem(requiredItem)
            : GameInventory.Instance.CheckItem(requiredItem);

        if (!interacted){
            missingItem?.Invoke();
            return false;
        }

        onUseItem?.Invoke();
        if (disableObject) gameObject.SetActive(false);
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
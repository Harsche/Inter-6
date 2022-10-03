using Inventory;
using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable{
    [SerializeField] private Item item;
    [SerializeField] private GameObject outline;

    public bool Interact(){
        gameObject.SetActive(false);
        GameInventory.Instance.AddItem(item);
        return true;
    }

    public void ToggleHighlight(bool active){
        outline.SetActive(active);
    }
}
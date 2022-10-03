using Inventory;
using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable{
    [SerializeField] private Item item;
    private bool highlight;

    private void LateUpdate(){
        if(!highlight) return;
        Debug.Log(name);
        highlight = false;
    }

    public bool Interact(){
        gameObject.SetActive(false);
        GameInventory.Instance.AddItem(item);
        return true;
    }

    public void ToggleHighlight(bool active){
        highlight = true;
    }
}
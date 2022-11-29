using Inventory;
using UnityEngine;

public class Flashlight : MonoBehaviour{
    [SerializeField] private Light flashlight;
    [SerializeField] private Item flashlightItem;

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Q)){ ToggleFlashlight(!flashlight.enabled); }
    }

    private void ToggleFlashlight(bool value){
        if (Player.Instance.Inventory.CheckItem(flashlightItem)){ flashlight.enabled = value; }
    }
}
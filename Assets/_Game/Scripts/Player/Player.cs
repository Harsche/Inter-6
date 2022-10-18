using Inventory;
using SaveGame;
using UnityEngine;

public class Player : MonoBehaviour{
    // [SerializeField] private GameInventory inventory;

    public static Player Instance{ get; private set; }
    [SerializeField] private GameInventory inventory;
    [SerializeField] private SaveManager saveManager;

    private void Awake(){
        if (Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;
        inventory.Setup();
        saveManager.Setup();
    }
}
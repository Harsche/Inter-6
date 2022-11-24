using Inventory;
using SaveGame;
using UnityEngine;

public class Player : MonoBehaviour{
    [SerializeField] private GameInventory inventory;
    [SerializeField] private SaveManager saveManager;
    
    [field: SerializeField] public Camera PlayerCamera{ get; private set; }
    [field: SerializeField] public PlayerMovement PlayerMovement{ get; private set; }

    public static Player Instance{ get; private set; }
    
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
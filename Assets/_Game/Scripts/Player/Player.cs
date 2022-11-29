using Inventory;
using SaveGame;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour{
    [SerializeField] private SaveManager saveManager;

    [field: SerializeField] public Camera PlayerCamera{ get; private set; }
    [field: SerializeField] public PlayerMovement PlayerMovement{ get; private set; }
    [field: SerializeField] public GameInventory Inventory{ get; private set; }
    [field: SerializeField] public Volume VolumeNormal{ get; private set; }
    [field: SerializeField] public Volume VolumeDark{ get; private set; }

    public static Player Instance{ get; private set; }

    private void Awake(){
        if (Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Inventory.Setup();
        saveManager.Setup();
    }
}
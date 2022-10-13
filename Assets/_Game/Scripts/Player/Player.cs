using UnityEngine;

public class Player : MonoBehaviour{
    // [SerializeField] private GameInventory inventory;

    public static Player Instance{ get; private set; }

    private void Awake(){
        if (Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
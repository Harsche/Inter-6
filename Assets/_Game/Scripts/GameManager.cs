using UnityEngine;

public class GameManager : MonoBehaviour{
    [SerializeField] private bool debug;


    public static GameManager Instance{ get; private set; }
    public static bool Debug{ get; private set; }
    public static bool IsGamePaused{ get; set; }

    private void Awake(){
        if (Instance != null){
            Destroy(gameObject);
            return;
        }

        Instance = this;
        OnValidate();
    }

    private void OnValidate(){
        Debug = debug;
    }
}
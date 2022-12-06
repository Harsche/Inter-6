using UnityEngine;

public class MainMenuSetup : MonoBehaviour{
    private void Awake(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
    }
}
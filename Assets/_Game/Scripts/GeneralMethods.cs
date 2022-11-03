using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "GeneralMethods", menuName = "General Methods", order = 0)]
public class GeneralMethods : ScriptableObject{
    public void ChangeScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    public void CloseGame(){
        Application.Quit();
    }

    public void DestroyScriptObject(MonoBehaviour obj){
        Destroy(obj);
    }

    public void SetMouseLocked(bool value){
        Cursor.visible = !value;
        Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
    }
    
    public void TogglePlayerInput(bool value){
        Player.Instance.PlayerMovement.stopInput = value;
    }
}
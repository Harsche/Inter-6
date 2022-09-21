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
}
using GameDialogs;
using UnityEngine;

public class Cheats : MonoBehaviour{
    private void Awake(){
#if UNITY_EDITOR
        ToggleInfiniteStamina();
#endif
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.P)){ ToggleInfiniteStamina(); }

        if (Input.GetKeyDown(KeyCode.Return)){ SkipDialog(); }
    }

    private void ToggleInfiniteStamina(){
        HUDManager.cheat = !HUDManager.cheat;
    }

    private void SkipDialog(){
        DialogManager.Instance.SkipDialog();
    }
}
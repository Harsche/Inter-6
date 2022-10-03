using UnityEngine;
using GameDialogs;

public class TestDialog : MonoBehaviour{
    [SerializeField] private TextAsset dialog;
    
    private void Update(){
        if(!Input.GetKeyDown(KeyCode.P)) return;
        Dialog.Instance.TriggerDialog(dialog);
    }
}
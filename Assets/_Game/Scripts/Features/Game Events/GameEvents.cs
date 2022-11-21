using GameDialogs;
using Inventory;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Game Events")]
public class GameEvents : ScriptableObject{
    public void TriggerDialog(TextAsset dialogJson){
        DialogManager.Instance.TriggerDialog(dialogJson);
    }

    public void CollectItem(Item item){
        GameInventory.Instance.AddItem(item);
    }

    // public void PlaySound(SoundPlayer soundPlayer){
    //     soundPlayer.PlaySound();
    // }

    // public void ViewItem(Item item){
    //     ItemDisplay.Instance.DisplayItem(item);
    // }

    public void ActivateOnDialogEnd(GameObject gameObject){
        DialogManager.Instance.OnDialogEnd += () =>  gameObject.SetActive(true);
    }

    public void LoadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    // public void TeleportPlayer(Transform teleportPosition){
    //     PlayerInteraction.Instance.PlayerRigidbody.position = teleportPosition.position;
    // }

    public void DontDestroy(GameObject gameObject){
        DontDestroyOnLoad(gameObject);
        gameObject.tag = "DontDestroy";
    }
}
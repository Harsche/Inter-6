using UnityEngine;


public class DoorSwitch : MonoBehaviour{
    [SerializeField] private Door door;
    [SerializeField] private Color locked;
    [SerializeField] private Color unlocked;
    [SerializeField] private Color highlighted;

    private Color switchColor;
    private MeshRenderer meshRenderer;

    private void Awake(){
        meshRenderer = GetComponent<MeshRenderer>();
        switchColor = door.Locked ? locked : unlocked;
        meshRenderer.material.color = switchColor;
    }

    private void OnMouseExit(){
        meshRenderer.material.color = switchColor;
    }

    private void OnMouseEnter(){
        meshRenderer.material.color = highlighted;
    }

    private void OnMouseDown(){
        door.UnlockDoor();
        switchColor = unlocked;
        meshRenderer.material.color = switchColor;
    }
}
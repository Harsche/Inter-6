using UnityEngine;


public class DoorTrigger : MonoBehaviour{
    [SerializeField] private Door door;

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")) door.SwitchDoor();
    }

    private void OnTriggerExit(Collider other){
        if (other.CompareTag("Player")) door.SwitchDoor();
    }
}
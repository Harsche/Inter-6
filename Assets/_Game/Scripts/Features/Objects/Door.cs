using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour{
    [SerializeField] private Transform door;
    [SerializeField] private GameObject trigger;
    [SerializeField] private Transform openedPosition;
    [SerializeField] private Transform closedPosition;
    [SerializeField] private float animationDuration;
    [SerializeField] private Ease animationEase;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip doorOpening;
    [SerializeField] private AudioClip doorClosing;
    [SerializeField] private MeshRenderer ledMeshRenderer;
    [SerializeField] private Material closedLed;
    [SerializeField] private Material openedLed;
    [field: SerializeField] public bool Locked{ get; private set; }

    private bool opened;

    private void Awake(){
        UpdateLedColor();
        if(!Locked) UnlockDoor();
    }

    private void UpdateLedColor(){
        ledMeshRenderer.material = Locked ? closedLed : openedLed;
    }

    public void SwitchDoor(){
        opened = !opened;
        audioSource.PlayOneShot(opened ? doorOpening : doorClosing);
        if (opened){
            door.transform.DOLocalMove(closedPosition.localPosition, animationDuration)
                .SetEase(animationEase);
            return;
        }
        door.transform.DOLocalMove(openedPosition.localPosition, animationDuration)
            .SetEase(animationEase);
    }

    public void UnlockDoor(){
        Locked = false;
        UpdateLedColor();
        trigger.SetActive(true);
    }
    
    public void LockDoor(){
        Locked = true;
        UpdateLedColor();
        trigger.SetActive(false);
    }
}
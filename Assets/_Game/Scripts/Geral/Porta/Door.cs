using System;
using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour{
    [SerializeField] private Transform door;
    [SerializeField] private GameObject trigger;
    [SerializeField] private Transform openedPosition;
    [SerializeField] private Transform closedPosition;
    [SerializeField] private float animationDuration;
    [SerializeField] private Ease animationEase;
    [field: SerializeField] public bool Locked{ get; private set; }

    private bool opened;

    private void Awake(){
        if(!Locked) UnlockDoor();
    }

    public void SwitchDoor(){
        opened = !opened;
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
        trigger.SetActive(true);
    }
}
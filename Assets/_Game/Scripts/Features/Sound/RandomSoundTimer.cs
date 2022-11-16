using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class RandomSoundTimer : MonoBehaviour{
    [SerializeField] private Vector2 minMaxTimeDifference;
    [SerializeField] private StudioEventEmitter eventEmitter;
    [SerializeField] private List<EventReference> possibleSounds;
    [SerializeField] private bool stopPrevious;
    private Coroutine soundCoroutine;

    private void OnEnable(){
        soundCoroutine = StartCoroutine(PlayRandomSound());
    }

    private void OnDisable(){
        StopCoroutine(soundCoroutine);
    }

    private IEnumerator PlayRandomSound(){
        while (true){
            float waitTime = Random.Range(minMaxTimeDifference.x, minMaxTimeDifference.y);
            yield return new WaitForSeconds(waitTime);
            int soundIndex = Random.Range(0, possibleSounds.Count);
            if(stopPrevious) eventEmitter.Stop();
            eventEmitter.ChangeEvent(possibleSounds[soundIndex]);
            eventEmitter.Play();
        }
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class WaitForVideo : MonoBehaviour{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private UnityEvent onVideoEnd;

    private void Start(){
        StartCoroutine(WaitForVideoEnd());
    }

    private IEnumerator WaitForVideoEnd(){
        yield return new WaitForSeconds((float) videoPlayer.clip.length);
        onVideoEnd?.Invoke();
    }
}
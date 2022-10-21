using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Video;

public class Intro : MonoBehaviour{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private Ease fadeEase = Ease.Linear;

    private void Awake(){
        canvasGroup.alpha = 1;
        StartCoroutine(WaitForIntro());
    }

    private IEnumerator WaitForIntro(){
        float introLength = (float) videoPlayer.clip.length;
        yield return new WaitForSeconds(introLength);
        videoPlayer.gameObject.SetActive(false);
        canvasGroup.DOFade(0f, fadeDuration)
            .SetEase(fadeEase)
            .OnComplete(() => canvasGroup.blocksRaycasts = false);
    }
}

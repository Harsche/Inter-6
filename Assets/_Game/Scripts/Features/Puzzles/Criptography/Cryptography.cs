using DG.Tweening;
using FMODUnity;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Cryptography : MonoBehaviour{
    [SerializeField] private string password;
    [SerializeField] private float paintDuration = 0.5f;
    [SerializeField] private Image panel;
    [SerializeField] private StudioEventEmitter eventEmitter;
    [SerializeField] private EventReference correctSound;
    [SerializeField] private EventReference incorrectSound;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Image timerCircle;
    [SerializeField] private UnityEvent onEnterCorrectPassword;
    [SerializeField] private UnityEvent onEnterIncorrectPassword;

    private Color panelDefaultColor;
    private Tweener timerTween;
    private string typedPassword;

    private void Awake(){
        panelDefaultColor = panel.color;
    }

    private void DoPaintPanel(bool isPasswordCorrect){
        const float alphaValue = 105f / 255f;
        Color finalColor = isPasswordCorrect ? Color.green : Color.red;
        finalColor.a *= alphaValue;
        panel.DOColor(finalColor, paintDuration / 2)
            .OnComplete(() => panel.DOColor(panelDefaultColor, paintDuration / 2));
    }
    
    private bool CheckIfCorrectPassword(){
        bool isPasswordCorrect = typedPassword == password;
        DoPaintPanel(isPasswordCorrect);
        eventEmitter.ChangeEvent(isPasswordCorrect ? correctSound : incorrectSound);
        eventEmitter.Stop();
        eventEmitter.Play();
        if (isPasswordCorrect){
            onEnterCorrectPassword?.Invoke();
            return true;
        }

        onEnterIncorrectPassword?.Invoke();
        ResetPassword();
        return false;
    }

    public void ResetPassword(){
        typedPassword = "";
        inputField.SetTextWithoutNotify("");
    }

    public void CheckPassword(){
        bool isPasswordCorrect = typedPassword == password;
        DoPaintPanel(isPasswordCorrect);
        eventEmitter.ChangeEvent(isPasswordCorrect ? correctSound : incorrectSound);
        eventEmitter.Stop();
        eventEmitter.Play();
        if (isPasswordCorrect){
            onEnterCorrectPassword?.Invoke();
            return;
        }

        onEnterIncorrectPassword?.Invoke();
        ResetPassword();
    }

    public void SetTypedPassword(string value){
        typedPassword = value;
    }

    public void StartTimer(float time){
        timerCircle.gameObject.SetActive(true);
        timerCircle.fillAmount = 1f;
        timerTween = timerCircle.DOFillAmount(0f, time)
            .SetEase(Ease.Linear)
            .OnComplete(() => {
                if (CheckIfCorrectPassword()) return;
                ResetPassword();
                StartTimer(time);
            });
    }

    public void StopTimer(){
        timerTween.Kill();
        timerCircle.gameObject.SetActive(false);
    }
}
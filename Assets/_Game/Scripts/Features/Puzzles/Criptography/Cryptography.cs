using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Cryptography : MonoBehaviour{
    [SerializeField] private string password;
    [SerializeField] private float paintDuration = 0.5f;
    [SerializeField] private Image panel;
    [SerializeField] private UnityEvent onEnterCorrectPassword;
    private Color panelDefaultColor;

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

    public void CheckPassword(){
        bool isPasswordCorrect = typedPassword == password;
        DoPaintPanel(isPasswordCorrect);
        if (isPasswordCorrect) onEnterCorrectPassword?.Invoke();
    }

    public void SetTypedPassword(string value){
        typedPassword = value;
    }
}
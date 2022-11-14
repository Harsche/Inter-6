using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Password : MonoBehaviour{
    [SerializeField] private int password;
    [SerializeField] private float paintDuration = 0.5f;
    [SerializeField] private Image panel;
    [SerializeField] private TextMeshProUGUI passwordText;
    [SerializeField] private UnityEvent onEnterCorrectPassword;
    [SerializeField] private UnityEvent onEnterIncorrectPassword;
    private Color panelDefaultColor;

    private int typedPassword;

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
        else onEnterIncorrectPassword?.Invoke();
    }

    public void AddNumberToPassword(int number){
        if (typedPassword.ToString().Length >= 4) return;
        char[] passwordCharArray = passwordText.text.ToCharArray();
        int charAsciiCode = 48 + number;
        for (int index = 0; index < passwordCharArray.Length; index++){
            char character = passwordCharArray[index];
            if (character != '_') continue;
            passwordCharArray[index] = (char) charAsciiCode;
            break;
        }

        string updatedPassword = new(passwordCharArray);
        passwordText.text = updatedPassword;
        typedPassword = int.Parse(updatedPassword.Replace(" ", "").Replace("_", ""));
    }

    public void ClearTypedPassword(){
        typedPassword = 0;
        passwordText.text = "_ _ _ _";
    }
}
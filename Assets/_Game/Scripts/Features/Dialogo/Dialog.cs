using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameDialogs{
    public class Dialog : MonoBehaviour{
        [SerializeField] private GameObject dialogBox;
        [SerializeField] private TextMeshProUGUI dialogText;
        [SerializeField] private string tessaHexColor;
        [SerializeField] private GameObject tessaDialogBox;
        [SerializeField] private TextMeshProUGUI tessaDialogText;
        [SerializeField] private string hillHexColor;
        [SerializeField] private GameObject hillDialogBox;
        [SerializeField] private TextMeshProUGUI hillDialogText;
        public event Action OnDialogEnd;

        private Coroutine dialogCoroutine;

        public static Dialog Instance{ get; private set; }

        private void Awake(){
            if (Instance != null){
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void StopDialog(){
            if (dialogCoroutine != null) StopCoroutine(dialogCoroutine);
            dialogText.text = "";
            if (dialogBox) dialogBox.SetActive(false);
        }

        public void TriggerDialog(TextAsset dialogJson){
            if (dialogCoroutine != null) StopCoroutine(dialogCoroutine);
            if (dialogBox) dialogBox.SetActive(true);
            List<SerializedDialog> dialogs = JsonUtility.FromJson<SerializeDialog>(dialogJson.ToString()).dialog;
            dialogCoroutine = StartCoroutine(DisplayDialog(dialogs));
        }

        private IEnumerator DisplayDialog(IEnumerable<SerializedDialog> dialogs){
            foreach (SerializedDialog t in dialogs){
                bool isTessaDialog = t.text.Contains(tessaHexColor);
                if (!isTessaDialog) t.text = t.text.Replace(hillHexColor, tessaHexColor);
                tessaDialogBox.SetActive(isTessaDialog);
                hillDialogBox.SetActive(!isTessaDialog);
                dialogText = isTessaDialog ? tessaDialogText : hillDialogText;
                dialogText.text = t.text;
                yield return new WaitForSeconds(t.time);
            }

            StopDialog();
            OnDialogEnd?.Invoke();
            OnDialogEnd = null;
        }
    }

    [Serializable]
    public class DialogText{
        [HideInInspector] public string dialogName;
        [TextArea] public string text;
        public float time;
        public Color dialogColor;

        public DialogText(string text, float time){
            this.text = text;
            this.time = time;
            dialogColor = Color.white;
        }
    }
}
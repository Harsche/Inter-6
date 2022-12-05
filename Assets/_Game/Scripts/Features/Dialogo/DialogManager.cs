using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GameDialogs{
    public class DialogManager : MonoBehaviour{
        [SerializeField] private GameObject dialogBox;
        [SerializeField] private TextMeshProUGUI dialogText;
        [SerializeField] private string tessaHexColor;
        [SerializeField] private GameObject tessaDialogBox;
        [SerializeField] private TextMeshProUGUI tessaDialogText;
        [FormerlySerializedAs("hillDialogBox"),SerializeField] private GameObject npcDialogBox;
        [SerializeField] private Image npcImage;
        [FormerlySerializedAs("hillDialogText"),SerializeField] private TextMeshProUGUI npcDialogText;
        [SerializeField] private CharacterData[] npcData;
        public event Action OnDialogEnd;
        
        private Coroutine dialogCoroutine;
        private int currentDialogIndex;
        private List<SerializedDialog> currentDialogs;
        private bool isShowingDialog;

        public static DialogManager Instance{ get; private set; }

        private void Awake(){
            if (Instance != null){
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
        
        private IEnumerator DisplayDialogCoroutine(List<SerializedDialog> dialogs){
            isShowingDialog = true;
            for (int i = currentDialogIndex; i < dialogs.Count; i++){
                currentDialogIndex = i;
                DisplayDialog(dialogs[i]);
                yield return new WaitForSeconds(dialogs[i].time);
            }


            OnDialogEnd?.Invoke();
            OnDialogEnd = null;
            StopDialog();
        }

        private void DisplayDialog(SerializedDialog t){
            bool isTessaDialog = t.text.Contains(tessaHexColor);
            if (!isTessaDialog){
                CharacterData charData = npcData.First(npc => t.text.Contains(npc.characterHexColor));
                t.text = t.text.Replace(charData.characterHexColor, tessaHexColor);
                npcImage.sprite = charData.characterSprite;
            }
            tessaDialogBox.SetActive(isTessaDialog);
            npcDialogBox.SetActive(!isTessaDialog);
            dialogText = isTessaDialog ? tessaDialogText : npcDialogText;
            dialogText.text = t.text;
        }

        public void StopDialog(){
            isShowingDialog = false;
            if (dialogCoroutine != null) StopCoroutine(dialogCoroutine);
            dialogText.text = "";
            if (dialogBox) dialogBox.SetActive(false);
        }

        public void TriggerDialog(TextAsset dialogJson){
            currentDialogIndex = 0;
            if (dialogCoroutine != null) StopCoroutine(dialogCoroutine);
            if (dialogBox) dialogBox.SetActive(true);
            currentDialogs = JsonUtility.FromJson<SerializeDialog>(dialogJson.ToString()).dialog;
            dialogCoroutine = StartCoroutine(DisplayDialogCoroutine(currentDialogs));
        }

        public void SkipDialog(){
            if(!isShowingDialog) return;
            currentDialogIndex++;
            if(dialogCoroutine != null) StopCoroutine(dialogCoroutine);
            dialogCoroutine = StartCoroutine(DisplayDialogCoroutine(currentDialogs));
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

    [Serializable]
    public class CharacterData{
        public Sprite characterSprite;
        public string characterHexColor;
    }
}
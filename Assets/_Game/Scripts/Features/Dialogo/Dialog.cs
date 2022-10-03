﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameDialogs{
    public class Dialog : MonoBehaviour{
        [SerializeField] private GameObject dialogBox;
        [SerializeField] private TextMeshProUGUI dialogText;
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
            if(dialogBox) dialogBox.SetActive(false);
        }

        public void TriggerDialog(TextAsset dialogJson){
            if (dialogCoroutine != null) StopCoroutine(dialogCoroutine);
            if(dialogBox) dialogBox.SetActive(true);
            List<DialogText> dialogs = JsonUtility.FromJson<SerializeDialog>(dialogJson.ToString()).dialog;
            dialogCoroutine = StartCoroutine(DisplayDialog(dialogs));
        }

        private IEnumerator DisplayDialog(IEnumerable<DialogText> dialogs){
            foreach (DialogText t in dialogs){
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
        public string text;
        public float time;
        public Color dialogColor;

        public DialogText(string text, float time){
            this.text = text;
            this.time = time;
            dialogColor = Color.white;
        }
    }
}
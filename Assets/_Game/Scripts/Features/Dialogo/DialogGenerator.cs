﻿using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GameDialogs{
    [CreateAssetMenu(menuName = "Dialog Generator")]
    public class DialogGenerator : ScriptableObject{
        [SerializeField] private DialogText[] dialogs;
        private string path;

        private void OnValidate(){
            path = Application.dataPath + "/_Game/Data/Dialogs/New_Dialog.json";
            UpdateDialogsNames();
        }

        public void GenerateJsonFile(){
            var serializeDialog = new SerializeDialog();
            foreach (DialogText dialog in dialogs){
                serializeDialog.dialog.Add(new DialogText(dialog.text, dialog.time));
            }
            for (int i = 0; i < dialogs.Length; i++){
                string colorHex = ColorUtility.ToHtmlStringRGB(dialogs[i].dialogColor);
                serializeDialog.dialog[i].text = $"<color=#{colorHex}>" + serializeDialog.dialog[i].text;
                serializeDialog.dialog[i].text += "</color>";
            }
            File.WriteAllText(path, JsonUtility.ToJson(serializeDialog, true));
        }

        private void UpdateDialogsNames(){
            for (int index = 0; index < dialogs.Length; index++){
                DialogText dialog = dialogs[index];
                dialog.dialogName = $"Dialog Line {index+1:00}";
            }
        }
    }

    [Serializable]
    public class SerializeDialog{
        public List<DialogText> dialog = new();
    }
}
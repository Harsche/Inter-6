using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameDialogs{
    [CreateAssetMenu(menuName = "Dialog Generator")]
    public class DialogGenerator : ScriptableObject{
        [SerializeField] private string dialogName = "New_Dialog";
        [SerializeField] private DialogText[] dialogs;
        private string path;

        private void OnValidate(){
            string fileName = string.IsNullOrEmpty(dialogName) ? "New_Dialog" : dialogName;
            path = Application.dataPath + $"/_Game/Data/Dialogs/{fileName}.json";
            UpdateDialogsNames();
        }

        public void GenerateJsonFile(){
            var serializeDialog = new SerializeDialog();
            foreach (DialogText dialog in dialogs){
                serializeDialog.dialog.Add(new SerializedDialog(dialog.text, dialog.time));
            }
            for (int i = 0; i < dialogs.Length; i++){
                string colorHex = ColorUtility.ToHtmlStringRGB(dialogs[i].dialogColor);
                serializeDialog.dialog[i].text = $"<color=#{colorHex}>" + serializeDialog.dialog[i].text;
                serializeDialog.dialog[i].text += "</color>";
            }
            File.WriteAllText(path, JsonUtility.ToJson(serializeDialog, true));
            AssetDatabase.Refresh();
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
        public List<SerializedDialog> dialog = new();
    }
    
    [Serializable]
    public class SerializedDialog{
        public string text;
        public float time;

        public SerializedDialog(string text, float time){
            this.text = text;
            this.time = time;
        }
    }
}
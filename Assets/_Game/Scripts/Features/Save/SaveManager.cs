using System.Collections.Generic;
using UnityEngine;

namespace SaveGame{
    [CreateAssetMenu(fileName = "Save Manager", menuName = "Save Manager", order = 0)]
    public class SaveManager : ScriptableObject{
        public static SaveManager Instance{ get; private set; }

        private Dictionary<string, object> saveData = new();

        public void Setup(){
            if (Instance != null) return;

            Instance = this;
        }

        public void SaveGame(){
            BayatGames.SaveGameFree.SaveGame.Save("data", saveData);
        }
        
        public void LoadGame(){
            saveData = BayatGames.SaveGameFree.SaveGame.Load<Dictionary<string, object>>("data");
        }

        public void SaveData(string key, object value){
            if (!saveData.ContainsKey(key)) saveData.Add(key, value);
            else saveData[key] = value;
        }

        public bool LoadData<T>(string key, out T value){
            value = default;
            if (!saveData.ContainsKey(key)) return false;
            value = (T) saveData[key];
            return true;
        }
    }
}
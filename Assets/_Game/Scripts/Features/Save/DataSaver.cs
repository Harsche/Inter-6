using System;
using UnityEngine;
using UnityEngine.Events;

namespace SaveGame{
    public class DataSaver : MonoBehaviour{
        // [SerializeField] private UnityEvent onLoadData;
        [SerializeField] private string destroyKey;

        private void Start(){
            
        }

        private void GetDestroyKey(){
            if(string.IsNullOrEmpty(destroyKey) || string.IsNullOrWhiteSpace(destroyKey)) return;
            if (!SaveManager.Instance.LoadData("destroy" + destroyKey, out bool destroy)) return;
            Destroy(gameObject);
        }
    }
}
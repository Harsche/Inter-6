using UnityEngine;
using UnityEngine.Events;

namespace SaveGame{
    public class DataSaver : MonoBehaviour{
        // [SerializeField] private UnityEvent onLoadData;
        [SerializeField] private string destroyKey;
        [SerializeField] private UnityEvent onLoad;

        private void Start(){
            onLoad?.Invoke();
        }

        private void GetDestroyKey(){
            if(string.IsNullOrEmpty(destroyKey) || string.IsNullOrWhiteSpace(destroyKey)) return;
            if (!SaveManager.Instance.LoadData("destroy" + destroyKey, out bool destroy)) return;
            Destroy(gameObject);
        }

        public bool SetBool(string key){
            if (!SaveManager.Instance.LoadData(key, out bool value)) return false;
            return value;
        }
    }
}
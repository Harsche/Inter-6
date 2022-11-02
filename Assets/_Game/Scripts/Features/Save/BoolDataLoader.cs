using System;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Events;

namespace SaveGame{
    public class BoolDataLoader : MonoBehaviour{
        [SerializeField] private string key;
        [SerializeField] private UnityEvent isTrue;
        [SerializeField] private UnityEvent isFalse;

        private void Start(){
            GetDataValue();
        }

        public void GetDataValue(){
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key)) return;
            if (!SaveManager.Instance.LoadData(key, out bool result))
                // throw new AttributeNotPresentInSaveException();
                SaveManager.Instance.SaveData(key, false);
            switch (result){
                case true:
                    isTrue?.Invoke();
                    break;
                case false:
                    isFalse?.Invoke();
                    break;
            }
        }
    }

    [Serializable]
    public class AttributeNotPresentInSaveException : Exception{
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public AttributeNotPresentInSaveException(){ }
        public AttributeNotPresentInSaveException(string message) : base(message){ }
        public AttributeNotPresentInSaveException(string message, Exception inner) : base(message, inner){ }

        protected AttributeNotPresentInSaveException(
            SerializationInfo info,
            StreamingContext context) : base(info, context){ }
    }
}
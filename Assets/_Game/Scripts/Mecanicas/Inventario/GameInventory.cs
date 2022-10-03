using System.Collections.Generic;
using UnityEngine;

namespace Inventory{
    public class GameInventory : MonoBehaviour{
        [SerializeField] private List<Item> items;

        public static GameInventory Instance{ get; private set; }

        private void Awake(){
            if (Instance != null){
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void AddItem(Item item){
            items.Add(item);
        }

        public bool CheckItem(Item item){
            return items.Contains(item);
        }
        
        public bool RemoveItem(Item item){
            if (!CheckItem(item)) return false;
            items.Remove(item);
            return true;
        }
    }

}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory{
    [CreateAssetMenu(menuName = "Inventory/Inventory", fileName = "Inventory", order = 0)]
    public class GameInventory : ScriptableObject{
        [SerializeField] private List<Item> items;

        public static GameInventory Instance{ get; private set; }

        private void OnEnable(){
            if (Instance != null){
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

using UnityEngine;

namespace Puzzles.Hack{
    public class Line : MonoBehaviour{
        public bool Connected{ get; private set; }
        private bool canCheckConnection;

        private void OnTriggerEnter2D(Collider2D col){
            if (!canCheckConnection || !col.gameObject.CompareTag("HackConnection")) return;
            Connected = true;
            ToggleCollision(true);
        }

        private void OnEnable(){
            canCheckConnection = true;
        }

        private void ToggleCollision(bool active){
            canCheckConnection = active;
        }

        public void Reset(){
            Connected = false;
        }

    }
}
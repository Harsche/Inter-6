using UnityEngine;

namespace Puzzles.Hack{
    public class Hexagons : MonoBehaviour{
        [SerializeField] private GameObject middleCircle;
        [SerializeField] private Line[] lines = new Line[6];
        [SerializeField] private Hexagons[] neighbours = new Hexagons[6];
        public bool Activated{ get; private set; }

        public void ToggleLine(bool value){
            Activated = value;
            middleCircle.SetActive(Activated);
        }

        public void SetupLines(){
            for (int index = 0; index < neighbours.Length; index++){
                Hexagons hexagon = neighbours[index];
            }
        }

        public void Rotate(bool counterClockwise){
            if (counterClockwise){
                
                return;
            }

            // foreach (var VARIABLE in ){
            //     
            // }
        }
    }
}
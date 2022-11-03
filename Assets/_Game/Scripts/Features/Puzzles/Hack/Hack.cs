using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Puzzles.Hack{
    public class Hack : MonoBehaviour{
        [SerializeField] private Hexagons[] hexagons;

        [ContextMenu("Randomize Puzzle")]
        public void RandomizePuzzle(){
            foreach (Hexagons hexagon in hexagons){
                hexagon.ToggleLine(Convert.ToBoolean(Random.Range(0, 2)));
            }
            
            // foreach (Hexagons hexagon in hexagons){
            //     hexagon.(Convert.ToBoolean(Random.Range(0, 2)));
            // }
        }
    }
}
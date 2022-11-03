using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Puzzles.Hack{
    public class Hack : MonoBehaviour{
        [SerializeField] private Hexagon[] hexagons;
        [SerializeField] private UnityEvent onSolveEvent;
        private bool puzzleSolved;

        [ContextMenu("Randomize Puzzle")]
        public void RandomizePuzzle(){
            foreach (Hexagon hexagon in hexagons) hexagon.ToggleHexagon(Convert.ToBoolean(Random.Range(0, 2)));

            foreach (Hexagon hexagon in hexagons) hexagon.ActivateBackupLines();

            foreach (Hexagon hexagon in hexagons) hexagon.SetupLines();
            
            foreach (Hexagon hexagon in hexagons) hexagon.RandomRotation();
        }

        private void Update(){
            if(Input.GetKeyDown(KeyCode.O)) onSolveEvent?.Invoke();
        }

        public void UpdatePuzzle(){
            CheckIfSolved();
        }

        public void CheckIfSolved(){
            if(puzzleSolved) return;
            puzzleSolved = hexagons.Where(hexagon => hexagon.Activated).All(hexagon => hexagon.CheckIfConnected());
            if(puzzleSolved) onSolveEvent?.Invoke();
        }
    }
}
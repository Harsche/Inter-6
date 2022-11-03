using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Puzzles.Hack{
    public class Hexagon : MonoBehaviour{
        [SerializeField] private GameObject middleCircle;
        [SerializeField] private Line[] lines = new Line[6];
        [SerializeField] private Hexagon[] neighbours = new Hexagon[6];
        [SerializeField] private Hack hackPuzzle;
        [SerializeField] private Image selectImage;
        public bool Activated{ get; private set; }

        private void Awake(){
            foreach (Line line in lines) line.SetupLine();
        }

        public void ToggleHexagon(bool value){
            Activated = value;
            middleCircle.SetActive(Activated);
        }

        // private void OnMouseEnter(){
        //     selectImage.DOFade(0.1f, 0.1f);
        // }
        //
        // private void OnMouseExit(){
        //     selectImage.DOFade(0.1f, 0.1f);
        // }

        public void ActivateBackupLines(){
            bool isAnyNeighbourActive = false;
            List<Hexagon> activatedNeighbours = new();
            foreach (Hexagon neighbour in neighbours){
                if (!neighbour) continue;
                activatedNeighbours.Add(neighbour);
                if (!neighbour.Activated) continue;
                isAnyNeighbourActive = true;
                break;
            }

            if (isAnyNeighbourActive) return;
            int neighbourCount = activatedNeighbours.Count;
            activatedNeighbours[Random.Range(0, neighbourCount)].ToggleHexagon(true);
        }

        public void SetupLines(){
            for (int index = 0; index < neighbours.Length; index++){
                Hexagon neighbour = neighbours[index];
                bool activateLine = neighbour && neighbour.Activated;
                lines[index].gameObject.SetActive(activateLine);
            }
        }

        private void UpdateHexagon(){
            List<Line> activatedLines = lines.Where(line => line.gameObject.activeSelf).ToList();
            foreach (Line line in activatedLines){
                line.Reset();
                line.CheckConnection();
            }
        }

        public bool CheckIfConnected(){
            UpdateHexagon();
            return lines.Where(line => line.gameObject.activeSelf).All(line => line.Connected);
        }

        public void Rotate(){
            List<Line> activatedLines = lines.Where(line => line.gameObject.activeSelf).ToList();
            foreach (Line line in activatedLines){
                line.Reset();
                Vector3 rotation = transform.localEulerAngles;
                rotation.z -= 60f;
                transform.DOLocalRotate(rotation, 0.1f)
                    .OnComplete(hackPuzzle.UpdatePuzzle);
            }
        }

        public void RandomRotation(){
            Transform myTransform = transform;
            const float rotationStep = -60f;
            int steps = Random.Range(1, 6);
            Vector3 rotation = myTransform.localEulerAngles;
            rotation.z += rotationStep * steps;
            myTransform.localEulerAngles = rotation;
            List<Line> activatedLines = lines.Where(line => line.gameObject.activeSelf).ToList();
            foreach (Line line in activatedLines) line.CheckConnection();
        }
    }
}
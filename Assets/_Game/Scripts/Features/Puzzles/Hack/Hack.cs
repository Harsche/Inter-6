using System;
using System.Linq;
using DG.Tweening;
using FMODUnity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Puzzles.Hack{
    public class Hack : MonoBehaviour{
        [SerializeField] private Canvas puzzleCanvas;
        [SerializeField] private Hexagon[] hexagons;
        [SerializeField] private Image timerCircle;
        [SerializeField] private Image panel;
        [SerializeField] private float paintDuration = 0.5f;
        [SerializeField] private StudioEventEmitter eventEmitter;
        [SerializeField] private EventReference correctSound;
        [SerializeField] private EventReference incorrectSound;
        [SerializeField] private UnityEvent onSolveEvent;

        private bool puzzleSolved;
        private Tweener timerTween;
        private Color panelDefaultColor;

        private void Awake(){
            panelDefaultColor = panel.color;
        }

        private void Start(){
            puzzleCanvas.worldCamera = Player.Instance.PlayerCamera;
        }

        private void Update(){
            if (Input.GetKeyDown(KeyCode.O)) onSolveEvent?.Invoke();
        }

        private bool CheckIfPuzzleIsSolved(){
            if (puzzleSolved){
                SolveFeedback();
                return true;
            }

            puzzleSolved = hexagons.Where(hexagon => hexagon.Activated).All(hexagon => hexagon.CheckIfConnected());
            SolveFeedback();
            if (puzzleSolved) onSolveEvent?.Invoke();
            return puzzleSolved;
        }

        private void SolveFeedback(){
            eventEmitter.ChangeEvent(puzzleSolved ? correctSound : incorrectSound);
            eventEmitter.Stop();
            eventEmitter.Play();
            DoPaintPanel(puzzleSolved);
        }

        private void DoPaintPanel(bool isPasswordCorrect){
            const float alphaValue = 105f / 255f;
            Color finalColor = isPasswordCorrect ? Color.green : Color.red;
            finalColor.a *= alphaValue;
            panel.DOColor(finalColor, paintDuration / 2)
                .OnComplete(() => panel.DOColor(panelDefaultColor, paintDuration / 2));
        }

        [ContextMenu("Randomize Puzzle")]
        public void RandomizePuzzle(){
            foreach (Hexagon hexagon in hexagons) hexagon.ToggleHexagon(Convert.ToBoolean(Random.Range(0, 2)));

            foreach (Hexagon hexagon in hexagons) hexagon.ActivateBackupLines();

            foreach (Hexagon hexagon in hexagons) hexagon.SetupLines();

            foreach (Hexagon hexagon in hexagons) hexagon.RandomRotation();
        }

        public void UpdatePuzzle(){
            CheckIfSolved();
        }

        public void CheckIfSolved(){
            if (puzzleSolved) return;
            puzzleSolved = hexagons.Where(hexagon => hexagon.Activated).All(hexagon => hexagon.CheckIfConnected());
            if (!puzzleSolved) return;
            SolveFeedback();
            onSolveEvent?.Invoke();
        }

        public void StartTimer(float time){
            timerCircle.gameObject.SetActive(true);
            timerCircle.fillAmount = 1f;
            timerTween = timerCircle.DOFillAmount(0f, time)
                .SetEase(Ease.Linear)
                .OnComplete(() => {
                    if (CheckIfPuzzleIsSolved()) return;
                    RandomizePuzzle();
                    StartTimer(time);
                });
        }

        public void StopTimer(){
            timerTween.Kill();
            timerCircle.gameObject.SetActive(false);
        }
    }
}
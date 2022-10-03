using System.Collections;
using UnityEngine;

namespace BehaviourTree{
    public class BehaviourTree: MonoBehaviour{
        [field: SerializeField] public Status Status{ get; protected set; } = Status.Running;
        [SerializeField] private Node root;
        
        private Coroutine behaviourTreeCoroutine;

        [ContextMenu("Run Behaviour Tree")]
        public void RunBehaviourTree(){
            behaviourTreeCoroutine = StartCoroutine(RunBehaviourTreeCoroutine());
        }
        
        [ContextMenu("Stop Behaviour Tree")]
        public void StopBehaviourTree(){
            if(behaviourTreeCoroutine != null) StopCoroutine(behaviourTreeCoroutine);
        }

        private IEnumerator RunBehaviourTreeCoroutine(){
            yield return root.Run(this);
            
            // while (Status != Status.Failure){
            //     yield return root.Run(this);
            // }
            // ReSharper disable once IteratorNeverReturns
        }
    }
}
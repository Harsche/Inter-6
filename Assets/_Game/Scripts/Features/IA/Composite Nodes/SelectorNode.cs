using System.Collections;
using UnityEngine;

namespace BehaviourTree{
    [CreateAssetMenu(menuName = "Behaviour Tree/Composite Nodes/Selector Node", fileName = "Selector Node", order = 0)]
    public class SelectorNode : Node{
        [SerializeField] private Node[] children;

        public override IEnumerator Run(BehaviourTree behaviourTree){
            foreach (Node child in children){
                yield return child.Run(behaviourTree);
                if(child.Status == Status.Failure) continue;
                Status = Status.Success;
                yield break;
            }
            Status = Status.Failure;
        }
    }
}
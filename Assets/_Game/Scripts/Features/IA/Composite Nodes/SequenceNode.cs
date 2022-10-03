using System.Collections;
using UnityEngine;

namespace BehaviourTree{
    [CreateAssetMenu(menuName = "Behaviour Tree/Composite Nodes/Sequence Node", fileName = "Sequence Node", order = 0)]
    public class SequenceNode : Node{
        [SerializeField] private Node[] children;

        public override IEnumerator Run(BehaviourTree behaviourTree){
            Status = Status.Running;
            foreach (Node child in children){
                yield return child.Run(behaviourTree);
                if(child.Status == Status.Success) continue;
                Status = Status.Failure;
                yield break;
            }
            Status = Status.Success;
        }
    }
}
using System.Collections;
using UnityEngine;

namespace BehaviourTree{
    [CreateAssetMenu(menuName = "Behaviour Tree/Decorator Nodes/Repeater Node", fileName = "Repeater Node", order = 0)]
    public class RepeaterNode : Node{
        [SerializeField] private Node child;
        [SerializeField] private int repeat;

        public override IEnumerator Run(BehaviourTree behaviourTree){
            Status = Status.Running;
            for (int i = 0; i < repeat; i++){
                yield return child.Run(behaviourTree);
            }

            Status = child.Status;
        }
    }
}
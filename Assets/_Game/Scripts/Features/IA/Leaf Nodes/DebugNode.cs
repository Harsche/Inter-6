using System.Collections;
using UnityEngine;

namespace BehaviourTree{
    [CreateAssetMenu(menuName = "Behaviour Tree/Leaf Nodes/Debug Node", fileName = "Debug Node", order = 0)]
    public class DebugNode : Node{
        [SerializeField] private string message;
        public override IEnumerator Run(BehaviourTree behaviourTree){
            Status = Status.Running;
            Debug.Log("Message: " + message);
            Status = Status.Success;
            yield break;
        }
    }
}
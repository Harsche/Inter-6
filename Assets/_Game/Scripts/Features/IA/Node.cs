using System;
using System.Collections;
using UnityEngine;

namespace BehaviourTree{
    public abstract class Node : ScriptableObject{
        public Status Status{ get; protected set; }

        public abstract IEnumerator Run(BehaviourTree behaviourTree);
    }

    [Serializable]
    public enum Status{
        Success,
        Failure,
        Running
    }
}
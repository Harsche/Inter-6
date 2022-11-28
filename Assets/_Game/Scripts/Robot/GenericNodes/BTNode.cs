using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BTNode
{
    public enum Status { RUNNING, SUCCESS, FAILURE}

    public Status status;

    abstract public IEnumerator Run(BehaviourTree2 bt);

    public void Print()
    {
        if(GameManager.Debug) Debug.Log(this.GetType().Name + ": " + status.ToString());
    }
}

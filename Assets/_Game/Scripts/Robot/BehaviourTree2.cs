using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree2 : MonoBehaviour
{
    public BTNode root;
    public IEnumerator Execute()
    {
        while(true)
        {
            yield return StartCoroutine(root.Run(this));
        }
    }
}

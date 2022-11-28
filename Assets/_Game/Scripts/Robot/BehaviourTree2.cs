using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourTree2 : MonoBehaviour
{
    public BTNode root;
    public RobotIA npcRef;
    [field: SerializeField] public Animator iaAnimator { get; private set; }
    [field: SerializeField] public NavMeshAgent iaNavMeshAgent { get; private set; }
    
    public IEnumerator Execute()
    {
        while(true)
        {
            yield return StartCoroutine(root.Run(this));
        }
    }
}

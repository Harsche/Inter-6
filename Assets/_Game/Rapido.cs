using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rapido : MonoBehaviour
{
    public Transform transformPlayer;

    public NavMeshAgent naveM;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        naveM.SetDestination(transformPlayer.position);
    }
}

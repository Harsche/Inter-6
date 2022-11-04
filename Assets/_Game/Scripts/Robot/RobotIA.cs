using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotIA : MonoBehaviour
{
    void Start()
    {
        BTSequence ataque = new BTSequence();

        ataque.children.Add(new BTPlayerProximo());
        ataque.children.Add(new BTAndarAtePlayer());
        ataque.children.Add(new BTAtacarPLayer());

        BehaviourTree2 bt = GetComponent<BehaviourTree2>();
        bt.root = ataque;

        StartCoroutine(bt.Execute());
    }

}

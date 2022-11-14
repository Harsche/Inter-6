using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotIA : MonoBehaviour
{
    void Start()
    {
        BTSequence patrulha = new BTSequence();

        patrulha.children.Add(new BTPatrulha());

        BTSequence ataque = new BTSequence();

        ataque.children.Add(new BTPlayerProximo());
        ataque.children.Add(new BTAndarAtePlayer());
        ataque.children.Add(new BTAtacarPlayer());

        BTSelector robot = new BTSelector();

        robot.children.Add(ataque);
        robot.children.Add(patrulha);

        BehaviourTree2 bt = GetComponent<BehaviourTree2>();
        bt.root = robot;

        StartCoroutine(bt.Execute());
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotIA : MonoBehaviour
{
    public List<Transform> npcPoints = new List<Transform>();
    public int pointIndex = 0;

    void Start()
    {
        BTSequence patrulha = new BTSequence();

        patrulha.children.Add(new BTSelecionarAlvo());
        //patrulha.children.Add(new BTOlharAlvo());
        patrulha.children.Add(new BTAndarAteAlvo());

        BTSequence ataque = new BTSequence();

        ataque.children.Add(new BTPlayerProximo());
        ataque.children.Add(new BTAndarAtePlayer());
        ataque.children.Add(new BTAtacarPlayer());

        BTSelector robot = new BTSelector();

        robot.children.Add(new BTStunnado());
        robot.children.Add(ataque);
        robot.children.Add(patrulha);

        BehaviourTree2 bt = GetComponent<BehaviourTree2>();
        bt.root = robot;

        StartCoroutine(bt.Execute());
    }

}

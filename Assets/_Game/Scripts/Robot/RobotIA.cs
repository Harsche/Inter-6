using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotIA : MonoBehaviour
{
    public KickCheck kickRef;

    public MeshRenderer olhoDir;
    public MeshRenderer olhoEsq;

    public Material normalOlho;
    public Material patrulhaOlho;
    public Material ataqueOlho;
    public Material stunOlho;

    public List<Transform> npcPoints = new List<Transform>();
    public List<Lixeira> lixeiras = new List<Lixeira>();

    public int pointIndex = 0;
    public int lixeiraIndex = 0;

    void Start(){
        kickRef = Player.Instance.PlayerMovement.kickRef;
        BTSequence patrulha = new BTSequence();

        patrulha.children.Add(new BTSelecionarAlvo());
        //patrulha.children.Add(new BTOlharAlvo());
        patrulha.children.Add(new BTAndarAteAlvo());

        BTSequence ataque = new BTSequence();

        ataque.children.Add(new BTPlayerProximo());
        ataque.children.Add(new BTAndarAtePlayer());
        ataque.children.Add(new BTAtacarPlayer());

        BTSequence verificaLixeiras = new BTSequence();

        verificaLixeiras.children.Add(new BTSelecionarLixeira());
        verificaLixeiras.children.Add(new BTLixeiraCaiu());
        verificaLixeiras.children.Add(new BTAndarAteLixeira());
        //verificaLixeiras.children.Add(new BTVerificandoLixeira());

        BTSelector robot = new BTSelector();

        robot.children.Add(new BTStunnado());
        robot.children.Add(ataque);
        robot.children.Add(verificaLixeiras);
        robot.children.Add(patrulha);

        BehaviourTree2 bt = GetComponent<BehaviourTree2>();
        bt.root = robot;

        StartCoroutine(bt.Execute());
    }

}

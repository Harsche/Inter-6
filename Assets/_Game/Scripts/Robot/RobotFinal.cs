using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFinal : MonoBehaviour
{
    private GameObject[] robotRef;
    private GameObject[] cabecaRef;
    private GameObject player;

    [SerializeField] private int quantRobos;

    void Start()
    {
        cabecaRef = new GameObject[quantRobos];
        robotRef = new GameObject[quantRobos];

        cabecaRef = GameObject.FindGameObjectsWithTag("Cabeca");
        robotRef = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        foreach (GameObject robot in robotRef)
        {
            Animator anim = robot.transform.GetChild(0).GetComponent<Animator>();

            anim.SetBool("isIdle", true);


            //robot.transform.LookAt(player.transform);
        }

        /*foreach (GameObject cabeca in cabecaRef)
        {
            cabeca.transform.LookAt(player.transform);
        }*/
    }
}

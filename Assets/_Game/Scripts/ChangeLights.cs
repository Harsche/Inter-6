using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLights : MonoBehaviour
{
    [SerializeField] private Material redLight;
    [SerializeField] private Material whiteLight;
    [SerializeField] private GameObject [] luzes;

    private bool redLights;

    void Start()
    {
        luzes = GameObject.FindGameObjectsWithTag("Light");
    }

    public void ChangeRedLights()
    {
        foreach(GameObject light in luzes)
        {
            MeshRenderer mesh = light.GetComponent<MeshRenderer>();
            mesh.material = redLight;

            Animator anim = light.GetComponent<Animator>();

            anim.enabled = true;
            anim.SetBool("piscando", true);
        }
    }

    public void ChangeWhiteLights()
    {
        foreach (GameObject light in luzes)
        {
            MeshRenderer mesh = light.GetComponent<MeshRenderer>();
            mesh.material = whiteLight;

            Animator anim = light.GetComponent<Animator>();
            anim.enabled = false;
            anim.SetBool("piscando", false);
        }
    }
}

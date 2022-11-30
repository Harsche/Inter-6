using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Varrendo : MonoBehaviour
{
    private Animator anim;
    private GameObject vassoura;

    void Start()
    {
        anim = GetComponent<Animator>();
        vassoura = transform.GetChild(0).gameObject;

        anim.SetBool("vassoura", true);
        vassoura.SetActive(true);
    }
}

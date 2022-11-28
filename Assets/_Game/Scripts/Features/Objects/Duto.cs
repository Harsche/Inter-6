using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duto : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerRef;
    [SerializeField] private GameObject dutoEscalavel;

    public bool duto1Aberto;
    public bool duto2Aberto;
    public bool duto3Aberto;
    public bool duto4Aberto;
    public bool duto5Aberto;
    public bool duto6Aberto;
    public bool duto7Aberto;

    private Animator animator;

    void Start()
    {
        duto1Aberto = false;
        duto2Aberto = false;
        duto3Aberto = false;
        duto4Aberto = false;
        duto5Aberto = false;
        duto6Aberto = false;
        duto7Aberto = false;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerRef.isClimb)
        {
            Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(raio, out hit))
            {
                if (hit.collider.CompareTag("Duto1"))
                {
                    if (duto1Aberto == false)
                    {
                        animator.SetBool("dutoAbrindo", true);
                        duto1Aberto = true;
                    }

                    if (duto1Aberto)
                    {
                        dutoEscalavel.SetActive(true);
                    }
                    else dutoEscalavel.SetActive(false);
                }

                if (hit.collider.CompareTag("Duto2"))
                {
                    if (duto2Aberto == false)
                    {
                        animator.SetBool("duto2Abrindo", true);
                        duto2Aberto = true;
                    }

                    if (duto2Aberto)
                    {
                        dutoEscalavel.SetActive(true);
                    }
                    else dutoEscalavel.SetActive(false);
                }

                if (hit.collider.CompareTag("Duto3"))
                {
                    if (duto3Aberto == false)
                    {
                        animator.SetBool("duto3Abrindo", true);
                        duto3Aberto = true;
                    }

                    if (duto3Aberto)
                    {
                        dutoEscalavel.SetActive(true);
                    }
                    else dutoEscalavel.SetActive(false);
                }

                if (hit.collider.CompareTag("Duto4"))
                {
                    if (duto4Aberto == false)
                    {
                        animator.SetBool("duto4Abrindo", true);
                        duto4Aberto = true;
                    }

                    if (duto4Aberto)
                    {
                        dutoEscalavel.SetActive(true);
                    }
                    else dutoEscalavel.SetActive(false);
                }

                if (hit.collider.CompareTag("Duto5"))
                {
                    if (duto5Aberto == false)
                    {
                        animator.SetBool("duto5Abrindo", true);
                        duto5Aberto = true;
                    }

                    if (duto5Aberto)
                    {
                        dutoEscalavel.SetActive(true);
                    }
                    else dutoEscalavel.SetActive(false);
                }

                if (hit.collider.CompareTag("Duto6"))
                {
                    if (duto6Aberto == false)
                    {
                        animator.SetBool("duto6Abrindo", true);
                        duto6Aberto = true;
                    }

                    if (duto6Aberto)
                    {
                        dutoEscalavel.SetActive(true);
                    }
                    else dutoEscalavel.SetActive(false);
                }

                if (hit.collider.CompareTag("Duto7"))
                {
                    if (duto7Aberto == false)
                    {
                        animator.SetBool("duto7Abrindo", true);
                        duto7Aberto = true;
                    }

                    if (duto7Aberto)
                    {
                        dutoEscalavel.SetActive(true);
                    }
                    else dutoEscalavel.SetActive(false);
                }
            }
        }
    }
}

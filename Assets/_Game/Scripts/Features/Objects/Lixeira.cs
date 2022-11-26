using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lixeira : MonoBehaviour
{
    public bool caiu = false;
    public float timer = 8f;
    private Transform transformPadrao;

    void Start()
    {
        transformPadrao = GetComponent<Transform>();

        transformPadrao.position = transform.position;
        transformPadrao.rotation = transform.rotation;

        caiu = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (caiu)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                transform.position = transformPadrao.position;
                transform.rotation = transformPadrao.rotation;

                timer = 8f;
                caiu = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            caiu = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            caiu = false;
        }
    }
}

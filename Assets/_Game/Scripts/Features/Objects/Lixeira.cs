using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lixeira : MonoBehaviour
{
    public bool caiu = false;
    public float timer = 8f;
    private Vector3 transformPosition;
    private Quaternion transformRotation;
    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        transformPosition = transform.position;
        transformRotation = transform.rotation;

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
                rigidBody.position = transformPosition;
                rigidBody.rotation = transformRotation;

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

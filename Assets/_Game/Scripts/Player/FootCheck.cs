using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootCheck : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerRef;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Esteira"))
        {
            playerRef.esteira = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Esteira"))
        {
            playerRef.esteira = false;
        }
    }

}

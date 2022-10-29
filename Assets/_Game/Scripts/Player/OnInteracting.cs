using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInteracting : MonoBehaviour
{


    void Start()
    {

    }

    void Update()
    {
       if(Input.GetKey(KeyCode.E))
       {
            Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(raio, out hit))
            {
                if (hit.transform.CompareTag("Interactable"))
                {
                    Destroy(hit.collider.gameObject);
                }
            }
       }
    }
}

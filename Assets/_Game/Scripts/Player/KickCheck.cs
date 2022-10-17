using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickCheck : MonoBehaviour
{
    [SerializeField] PlayerMovement playerRef;
    
    private bool kicked;

    void Start()
    {
        kicked = false;
    }

    public void Kick()
    {
        if (playerRef.isKick)
        {
            playerRef.animator.SetBool("isKicking", true);
        }

        if (!playerRef.isKick)
        {
            playerRef.animator.SetBool("isKicking", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            kicked = true;

            Destroy(collision.gameObject);
        }
    }


}

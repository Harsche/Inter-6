using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickCheck : MonoBehaviour
{
    [SerializeField] PlayerMovement playerRef;
    [SerializeField] HUDManager hudRef;

    [SerializeField] private float kickForce = 5f;

    private bool kicked;

    void Start()
    {
        kicked = false;
    }

    public void Kick()
    {
        if (playerRef.isKick && kicked == false && hudRef.staminaValue > 0)
        {
            playerRef.animator.SetBool("isKicking", true);
        }

        if (!playerRef.isKick)
        {
            playerRef.animator.SetBool("isKicking", false);
            kicked = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            kicked = true;

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Kickable"))
        {
            kicked = true;

            collision.rigidbody.AddForce(transform.forward * kickForce);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        kicked = false;
    }


}

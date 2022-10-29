using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickCheck : MonoBehaviour
{
    [SerializeField] PlayerMovement playerRef;
    [SerializeField] HUDManager hudRef;
    [SerializeField] GameObject kickDirection;

    [SerializeField] private float kickForce = 20f;

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

            collision.rigidbody.AddForceAtPosition(kickDirection.transform.forward * kickForce, kickDirection.transform.position);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        kicked = false;
    }


}

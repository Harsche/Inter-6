using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickCheck : MonoBehaviour
{
    [SerializeField] PlayerMovement playerRef;
    [SerializeField] HUDManager hudRef;
    [SerializeField] GameObject kickDirection;
    [SerializeField] Animator playerAnim;

    [SerializeField] private float kickForce = 20f;

    public bool kicked;
    public bool kickedEnemy;

    void Start()
    {
        kicked = false;
        kickedEnemy = false;
    }

    public void Kick()
    {
        if (playerRef.isKick && kicked == false && hudRef.staminaValue > 0)
        {
            playerRef.animator.SetBool("isKicking", true);
            if (playerRef.levouDano)
            {
                playerAnim.SetLayerWeight(playerAnim.GetLayerIndex("DamageKick"), 1f);
            }
        }
        else
        {
            playerAnim.SetLayerWeight(playerAnim.GetLayerIndex("DamageKick"), 0f);
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
            kickedEnemy = true;

            print(kickedEnemy);
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

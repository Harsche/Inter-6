using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] GameObject staminaIconActived;
    [SerializeField] GameObject staminaIcon;
    [SerializeField] PlayerMovement playerRef;

    private float staminaValue;

    private void Start()
    {
        staminaValue = 15;
    }

    void Update()
    {
        // StartCoroutine(Stamina());

        //print(staminaValue);
        //print(playerRef.useStamina);
    }
    
    IEnumerator Stamina()
    {
        if (staminaValue <= 0)
        {
            yield break;
        }

        if (playerRef.useStamina) //Come�a a consumir Stamina
        {
            staminaIcon.SetActive(false);
            staminaIconActived.SetActive(true);

            if (staminaValue > 0)
            {
                for(int i = 15; i > 0; i--)
                {
                    staminaValue--;
                    yield return new WaitForSeconds(10f);
                }
            }
            else yield break;
        } 
        /*else
        {
            staminaIcon.SetActive(true);
            staminaIconActived.SetActive(false);
        }*/
    }
}

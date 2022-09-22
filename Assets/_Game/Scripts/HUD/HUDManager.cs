using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] GameObject staminaIconActived;
    [SerializeField] GameObject staminaIcon;

    private float staminaValue;

    private void Start()
    {
        staminaValue = 15;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) //Verificando icone da Stamina
        {
            staminaIcon.SetActive(false);
            staminaIconActived.SetActive(true);
            //StartCoroutine(Stamina());
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            staminaIcon.SetActive(true);
            staminaIconActived.SetActive(false);
        }
        print(staminaValue);
    }
    
    IEnumerator Stamina()
    {
        /*for (float i = staminaValue; i > 0; i--)
        {
            staminaValue = i;
        }*/
        
        if(staminaValue > 0) staminaValue--;
        print(staminaValue);

        yield return new WaitForSeconds(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerRef;

    [SerializeField] private float staminaValue = 15f;
    [SerializeField] private float staminaReducao = 0.1f;
    [SerializeField] private float lifeReducao = 0.1f;
    [SerializeField] private float staminaRecarga = 0.1f;

    [SerializeField] public float playerLife = 100f;

    private Coroutine staminaCoroutine;
    private Coroutine recargaCoroutine;
    private Coroutine danoCoroutine;

    private bool staminaAcabou;

    [SerializeField] private Slider sliderStamina;
    [SerializeField] private Slider sliderLife;
    [SerializeField] private Image lowLifeScreen;
    [SerializeField] private Image damageScreen;

    void Update()
    {
        Stamina();
        Life();
    }

    void Life()
    {
        if (danoCoroutine == null)
        {
            danoCoroutine = StartCoroutine(ReduzLife());
        }
        else
        {
                StopCoroutine(ReduzLife());
                //danoCoroutine = null;
        }

        if (playerLife <= 30f)
        {
            lowLifeScreen.gameObject.SetActive(true);
        }

        if (playerLife <= 0f)
        {
            playerRef.animator.SetBool("isDead", true);
            print("Game Over");
        }
    }

    void Stamina()
    {
        if (playerRef.isRun && staminaCoroutine == null)
        {
            staminaCoroutine = StartCoroutine(ReduzStamina());

            if (recargaCoroutine != null)
            {
                StopCoroutine(recargaCoroutine);
                recargaCoroutine = null;
            }
        }
        else
        {
            if (staminaCoroutine != null)
            {
                StopCoroutine(staminaCoroutine);
                staminaCoroutine = null;
            }

            if (recargaCoroutine == null)
            {
                recargaCoroutine = StartCoroutine(RecarregaStamina());
            }
        }

        print("stamina" + staminaValue);
    }

    IEnumerator ReduzStamina()
    {
        while (true)
        {
            if (staminaValue <= 0)
            {
                staminaAcabou = true;

                yield break;
            }

            while (staminaValue > 0)
            {
                staminaAcabou = false;

                staminaValue -= 0.1f * staminaReducao;
                sliderStamina.value = staminaValue;

                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    IEnumerator RecarregaStamina()
    {
        while (true)
        {
            if (!staminaAcabou)
            {
                yield break;
            }

            yield return new WaitForSeconds(15f);

            while (staminaAcabou)
            {
                staminaValue += 0.1f * staminaRecarga;
                sliderStamina.value = staminaValue;

                if (staminaValue == 15f)
                {
                    staminaAcabou = false;

                    yield break;
                }

                yield return new WaitForSeconds(1f);
            }
        }
    }

    IEnumerator ReduzLife()
    {
        while (true)
        {
            if (playerLife <= 0)
            {
                yield break;
            }

            while (playerLife > 0)
            {
                if (playerRef.animator.GetBool("isDamaged"))
                {
                    damageScreen.gameObject.SetActive(true);
                    playerLife -= 0.1f * lifeReducao;
                    sliderLife.value = playerLife;

                    yield return new WaitForSeconds(0.8f);

                    damageScreen.gameObject.SetActive(false);

                    yield return new WaitForSeconds(1f);
                }
                else yield break;

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}

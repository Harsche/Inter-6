using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerRef;

    [SerializeField] private float staminaRunReducao = 0.1f;
    [SerializeField] private float staminaKickReducao = 0.1f;
    [SerializeField] private float lifeReducao = 0.1f;
    [SerializeField] private float staminaRecarga = 0.1f;

    [SerializeField] public float staminaValue = 15f;
    [SerializeField] public float playerLife = 100f;

    [SerializeField] private Slider sliderStamina;
    [SerializeField] private Slider sliderLife;
    [SerializeField] private Image lowLifeScreen;
    [SerializeField] private Image damageScreen;
    [SerializeField] private Image gameOverScreen;

    private Coroutine staminaCoroutine;
    private Coroutine recargaCoroutine;
    private Coroutine danoCoroutine;

    private bool staminaAcabou;

    void Update()
    {
        Stamina();
        Life();
    }

    void Life()
    {
        if (playerRef.levouDano)
        {
            if (danoCoroutine == null)
            {
                danoCoroutine = StartCoroutine(ReduzLife());
            }
        } else
        {
            if (danoCoroutine != null)
            {
                StopCoroutine(danoCoroutine);
                danoCoroutine = null;
            }
        }

        if (playerLife <= 30f)
        {
            lowLifeScreen.gameObject.SetActive(true);
        }

        if (playerRef.isDead)
        {
            lowLifeScreen.gameObject.SetActive(false);
            gameOverScreen.gameObject.SetActive(true);
        }
    }

    void Stamina()
    {
        if (playerRef.isRun || playerRef.isKick && staminaCoroutine == null)
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

                if(playerRef.isRun)
                {
                    staminaValue -= 0.1f * staminaRunReducao;
                }
                if (playerRef.isKick)
                {
                    staminaValue -= 0.1f * staminaKickReducao;
                }

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
                StartCoroutine(FeedbackDano());
                
                playerLife -= 0.1f * lifeReducao;
                sliderLife.value = playerLife;

                yield return new WaitForSeconds(1f);
            }
        }
    }

    IEnumerator FeedbackDano()
    {
        damageScreen.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.8f);

        damageScreen.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField] PlayerMovement playerRef;

    [SerializeField] private float staminaRunReducao = 0.1f;
    [SerializeField] private float staminaKickReducao = 0.1f;
    [SerializeField] private float lifeReducao = 0.1f;
    [SerializeField] private float staminaStartRechargeTime = 10f;
    [SerializeField] private float staminaRecarga = 0.1f;

    [SerializeField] public float staminaValue = 15f;
    [SerializeField] public float playerLife = 100f;
    [SerializeField] public int bandAid;

    [SerializeField] private Slider sliderStamina;
    [SerializeField] private Slider sliderLife;
    [SerializeField] private Image lowLifeScreen;
    [SerializeField] private Image damageScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject hudScreen;

    [SerializeField] private Image lanternaIcon;
    [SerializeField] private Image ferramentaIcon;
    [SerializeField] private Image bandAidIcon;
    [SerializeField] private TextMeshProUGUI numbBandAid;

    [SerializeField] private StudioEventEmitter takeBreathEventEmitter;

    private Coroutine staminaCoroutine;
    private Coroutine recargaCoroutine;
    private Coroutine danoCoroutine;

    private bool staminaAcabou;
    public static bool cheat;
    public static HUDManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);

            return;
        }

        Instance = this;
    }

    public void Curar(int quantidade)
    {
        bandAid += quantidade;

        numbBandAid.text = bandAid.ToString();
    }

    void Update()
    {
        if (playerRef.usouBand && bandAid > 0)
        {
            Curando();
        }

        if (cheat) return;
        Stamina();
        Life();

        if (playerRef.isKick)
        {
            staminaValue -= 0.1f * staminaKickReducao;
            sliderStamina.value = staminaValue;
        }
    }

    void Life()
    {
        if (playerRef.levouDano)
        {
            if (danoCoroutine == null)
            {
                danoCoroutine = StartCoroutine(ReduzLife());
            }
        }
        else
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
        else lowLifeScreen.gameObject.SetActive(false);

        if (playerRef.isDead)
        {
            lowLifeScreen.gameObject.SetActive(false);
            hudScreen.gameObject.SetActive(false);
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
    }

    IEnumerator ReduzStamina()
    {
        while (true)
        {
            if (cheat) yield break;
            if (staminaValue <= 0)
            {
                staminaAcabou = true;

                if (takeBreathEventEmitter.IsPlaying()) yield break;
                takeBreathEventEmitter.Stop();
                takeBreathEventEmitter.Play();

                yield break;
            }

            if (staminaValue > 0)
            {
                staminaAcabou = false;

                if (playerRef.isRun && playerRef.moveInput.sqrMagnitude > 0)
                {
                    staminaValue -= 0.1f * staminaRunReducao;
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
            /*if (!staminaAcabou)
            {
                yield break;
            }*/

            if(staminaValue >= 100f) yield break;

            yield return new WaitForSeconds(staminaStartRechargeTime);

            while (staminaValue < 100f)
            {
                staminaValue += 0.1f * staminaRecarga;
                sliderStamina.value = staminaValue;

                if (staminaValue == 30f)
                {
                    staminaAcabou = false;

                    yield break;
                }

                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    IEnumerator ReduzLife()
    {
        while (true)
        {
            if (cheat) yield break;
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

    public void Curando()
    {
        playerLife += 25f;
        sliderLife.value = playerLife;

        Curar(-1);

        if (bandAid <= 0)
        {
            bandAidIcon.color = new Color(1f, 1f, 1f, 80f/255f); 
        }
        playerRef.usouBand = false;
    }

    public void AtivarLanterna()
    {
        lanternaIcon.color = Color.white;
    }

    public void AtivarFerramenta()
    {
        ferramentaIcon.color = Color.white;
    }

    public void AtivarBandAid()
    {
        bandAidIcon.color = Color.white;
    }
}

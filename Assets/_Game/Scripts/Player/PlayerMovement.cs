using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public Animator animator;

    [SerializeField] CameraMovement cameraRef;
    [SerializeField] KickCheck kickRef;
    [SerializeField] HUDManager hudRef;
    [SerializeField] FootCheck footRef;

    [SerializeField] GameObject esteiraRef;

    [SerializeField] float gravity;
    [SerializeField] float jumpForce;

    [SerializeField] float normalHCollision = 1.8f;
    [SerializeField] float crawlingHCollision = 0.8f;
    [SerializeField] float deadHCollision = 0f;
    [SerializeField] float esteiraVel = 100f;

    [SerializeField] Vector3 normalCollision = new Vector3(0, 0.91f, 0);
    [SerializeField] Vector3 crawlingCollision = new Vector3(0, 0.37f, 0);
    [SerializeField] Vector3 deadCollision = new Vector3(0, 0.45f, 0);

    [SerializeField] private StudioEventEmitter stepsEventEmitter;
    [SerializeField] private EventReference walkSound;

    private Vector3 movement;
    private float moveY;
    public bool esteira = false;

    private bool isMoving;
    private bool isCrawl;
    private bool isClimb;
    public bool isKick;
    public bool isRun;
    private bool isSlowly;
    private bool isJump;
    public bool isDead = false;

    public bool levouDano;
    public bool possibleClimb = false;
    public bool climbingArea = false;
    public bool crawlingTrans = false;
    public bool climbImpulse = false;

    private float isMovingH;
    private float isMovingV;

    public bool stopInput;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        GetInput();

        if (hudRef.playerLife <= 0)
        {
            isDead = true;
            Dead();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            return;
        }

        if (isMoving) PlayerVelocity();
        else animator.SetFloat("Velocity", 0f);

        Crawl();
        kickRef.Kick();

        PlayerMove();

        cameraRef.CameraMove();
    }

    void GetInput() //Setando inputs
    {
        isMovingH = 0f;
        isMovingV = 0f;
        isMoving = false;
        if (stopInput) return;

        cameraRef.valueCamY = Input.GetAxis("Mouse Y");
        cameraRef.valueCamX = Input.GetAxis("Mouse X");

        isMovingH = Input.GetAxis("Horizontal");
        isMovingV = Input.GetAxis("Vertical");

        isMoving = Input.GetButton("Horizontal") || Input.GetButton("Vertical");
        isCrawl = Input.GetKey(KeyCode.C);
        isClimb = Input.GetKey(KeyCode.E);
        isKick = Input.GetKeyDown(KeyCode.F);
        isRun = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        isSlowly = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
        isJump = Input.GetButton("Jump");
    }

    void PlayerMove() //Associando valores de movimentacao
    {
        movement = isMovingV * transform.forward;

        movement += isMovingH * transform.right;

        movement *= animator.GetFloat("Velocity");

        //Jump();
        Climb();

        if (animator.GetBool("crawlingTransArea") == false) Gravity();

        if (esteira && footRef)
        {
            StartCoroutine(Esteira());
        }
        else StopCoroutine(Esteira());

        movement.y = moveY;

        movement *= Time.deltaTime;

        controller.Move(movement);
    }

    void PlayerVelocity() //Definindo variacoes na velocidade de movimento
    {
        if (!isMoving) return;

        if (isCrawl)
        {
            animator.SetFloat("Velocity", 3f);
        }
        else if (isRun && hudRef.staminaValue > 0)
        {
            animator.SetFloat("Velocity", 6.5f);
        }
        else if (isSlowly)
        {
            animator.SetFloat("Velocity", 1.8f);
        }
        else //velocidade normal de caminhada
        {
            animator.SetFloat("Velocity", 3.5f);
        }
    }

    void Crawl()
    {
        if (isCrawl)
        {
            animator.SetBool("isCrawling", true);

            controller.height = crawlingHCollision;
            controller.center = crawlingCollision;
        }

        if (!isCrawl)
        {
            animator.SetBool("isCrawling", false);

            controller.height = normalHCollision;
            controller.center = normalCollision;
        }
    }

    void Climb()
    {
        if (possibleClimb)
        {
            controller.height = crawlingHCollision;
            controller.center = crawlingCollision;

            if (isClimb)
            {
                animator.SetBool("isClimbing", true);
            }
            else animator.SetBool("isClimbing", false);

            if (animator.GetBool("crawlingTransArea"))
            {
                if (isClimb)
                {
                    moveY = 1f;

                    if (climbImpulse)
                    {
                        movement.z += 0.1f;
                        controller.Move(movement);
                    }

                }

            }
            else moveY = 0f;
        }

        if (animator.GetBool("climbingArea"))
        {
            animator.SetBool("isCrawling", true);

            controller.height = crawlingHCollision;
            controller.center = crawlingCollision;
        }
    }

    /*float Jump()
    {
        if (controller.isGrounded)
        {
            moveY = 0f;

            if (isJump && !isCrawl)
            {
                moveY += jumpForce;
                animator.SetBool("isJumping", true);
            }
            else animator.SetBool("isJumping", false);
        }

        return moveY;
    }*/

    void Dead()
    {
        if (isDead)
        {
            stopInput = true;

            animator.SetBool("isDead", true);
            controller.height = deadHCollision;
            controller.center = deadCollision;
        }
        else return;
    }

    float Gravity()
    {
        moveY += gravity;
        return moveY;
    }

    IEnumerator Esteira()
    {
        movement += esteiraRef.transform.forward * (Time.deltaTime * esteiraVel);

        yield return new WaitForSeconds(0.001f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            levouDano = true;
            animator.SetBool("isDamaged", true);
        }

        if (other.gameObject.CompareTag("ClimbingArea"))
        {
            possibleClimb = true;
        }

        if (other.gameObject.CompareTag("CrawlingArea"))
        {
            animator.SetBool("climbingArea", true);
        }

        if (other.gameObject.CompareTag("CrawlingTransition"))
        {
            animator.SetBool("crawlingTransArea", true);
        }

        if (other.gameObject.CompareTag("ClimbCollider"))
        {
            climbImpulse = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            levouDano = false;
            animator.SetBool("isDamaged", false);
        }

        if (other.gameObject.CompareTag("ClimbingArea"))
        {
            possibleClimb = false;
        }

        if (other.gameObject.CompareTag("CrawlingArea"))
        {
            animator.SetBool("climbingArea", false);
        }

        if (other.gameObject.CompareTag("CrawlingTransition"))
        {
            animator.SetBool("crawlingTransArea", false);
        }

        if (other.gameObject.CompareTag("ClimbCollider"))
        {
            climbImpulse = false;
        }
    }

    public void PlayWalkSound()
    {
        stepsEventEmitter.SetParameter("Parameter 2", 0);
        stepsEventEmitter.Play();
    }

    public void PlayRunSound(){
        stepsEventEmitter.SetParameter("Parameter 2", 1);
        stepsEventEmitter.Play();
    }

    public void SetEsteiraVel(float value){
        esteiraVel = value;
    }

    public void DesativaCollider()
    {
        kickRef.colliderChute.enabled = false;
    }

    public void AtivaCollider()
    {
        kickRef.colliderChute.enabled = true;
    }
}
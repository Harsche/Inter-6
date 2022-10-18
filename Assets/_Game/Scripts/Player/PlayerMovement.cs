using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public Animator animator;

    [SerializeField] CameraMovement cameraRef;
    [SerializeField] KickCheck kickRef;
    [SerializeField] HUDManager hudRef;
    [SerializeField] float gravity;
    [SerializeField] float jumpForce;

    private Vector3 movement;
    private float moveY;

    private bool isMoving;
    private bool isCrawl;
    public bool isKick;
    public bool isRun;
    private bool isSlowly;
    private bool isJump;

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

        if (isMoving) PlayerVelocity();
        else animator.SetFloat("Velocity", 0f);

        Crawl();
        kickRef.Kick();

        PlayerMove();

        cameraRef.CameraMove();
    }

    void GetInput() //Setando inputs
    {
        if (stopInput) return;

        cameraRef.valueCamY = Input.GetAxis("Mouse Y");
        cameraRef.valueCamX = Input.GetAxis("Mouse X");

        isMovingH = Input.GetAxis("Horizontal");
        isMovingV = Input.GetAxis("Vertical");

        isMoving = Input.GetButton("Horizontal") || Input.GetButton("Vertical");
        isCrawl = Input.GetKey(KeyCode.C);
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

        Gravity();

        Jump();

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
        else if (isRun)
        {
            animator.SetFloat("Velocity", 6.5f);
        }
        else if (isSlowly)
        {
            animator.SetFloat("Velocity", 1.8f);
        }
        else //velocidade normal de caminhada
        {
            animator.SetFloat("Velocity", 4f);
        }
    }

    void Crawl()
    {
        if (isCrawl)
        {
            animator.SetBool("isCrawling", true);

            controller.height = 0.8f;
            controller.center = new Vector3(0, 0.37f, 0);
        }

        if (!isCrawl)
        {
            animator.SetBool("isCrawling", false);

            controller.height = 1.8f;
            controller.center = new Vector3(0, 0.91f, 0);
        }
    }

    float Jump()
    {
        if (controller.isGrounded){
            moveY = 0f;

            if (isJump && !isCrawl){
                moveY += jumpForce;
                animator.SetBool("isJumping", true);
            }
            else animator.SetBool("isJumping", false);
        }

        return moveY;
    }

    float Gravity()
    {
        moveY += gravity;
        return moveY;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            animator.SetBool("isDamaged", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            animator.SetBool("isDamaged", false);
        }
    }
}
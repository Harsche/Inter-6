using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    [SerializeField] GameObject head;
    [SerializeField] float sensitivityCam;
    [SerializeField] float gravity;
    [SerializeField] float jumpForce;

    private Vector3 movement;
    private float camY;
    private float moveY;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")) //Definindo variações na velocidade de movimento
        {
           
            if (animator.GetBool("isCrawling"))
            {
                animator.SetFloat("Velocity", 3f);
            } else
            if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                animator.SetFloat("Velocity", 1f);
            } else 
            if(Input.GetKey(KeyCode.LeftAlt))
            {
                animator.SetFloat("Velocity", 6f);
            } else animator.SetFloat("Velocity", 3f);

        } else animator.SetFloat("Velocity", 0f);

        movement = Input.GetAxis("Vertical") * transform.forward; //Associando valores de movimentação

        movement += Input.GetAxis("Horizontal") * transform.right;

        movement *= animator.GetFloat("Velocity");

        Gravity();

        Jump();

        movement.y = moveY;

        movement *= Time.deltaTime;

        controller.Move(movement);

        Crawl();
        CameraMovement();
    }

    void Crawl()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            animator.SetBool("isCrawling", true);
            controller.height = 0.8f;
            controller.center = new Vector3(0, 0.37f, 0);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
        {
            animator.SetBool("isCrawling", false);
            controller.height = 1.8f;
            controller.center = new Vector3(0, 0.91f, 0);
        }
    }

    float Jump()
    {
        if(controller.isGrounded)
        {
            moveY = 0f;

            if (Input.GetButton("Jump"))
            {
                moveY += jumpForce;
                animator.SetBool("isJumping", true);
            } else animator.SetBool("isJumping", false);
        }

        return moveY;
    }

    void CameraMovement()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityCam * Time.deltaTime, 0); //Movimento de camera horizontal

        camY += -Input.GetAxis("Mouse Y") * sensitivityCam * Time.deltaTime; //Movimento da camera vertical
        camY = Mathf.Clamp(camY, -75, 75);
        head.transform.localEulerAngles = new Vector3(camY, 0, 0);
    }

    float Gravity()
    {
        moveY += gravity;

        return moveY;
    }
}

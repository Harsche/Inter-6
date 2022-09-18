using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    [SerializeField] GameObject head;
    private Vector3 movement;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            animator.SetFloat("Velocity", 8f);
        } else animator.SetFloat("Velocity", 0f);

        movement = Input.GetAxis("Vertical") * transform.forward;

        movement += Input.GetAxis("Horizontal") * transform.right;

        movement *= animator.GetFloat("Velocity") * Time.deltaTime;

        controller.Move(movement);

        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);

        head.transform.Rotate(Input.GetAxis("Mouse Y"), 0, 0);

        Abaixar();

    }

    void Abaixar()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            head.transform.position -= new Vector3(0, 0.5f, 0);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
        {
            head.transform.position -= new Vector3(0, -0.5f, 0);
        }
    }
}

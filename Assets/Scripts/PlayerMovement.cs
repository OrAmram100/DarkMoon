using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -13f;
    public float jumpHeight = 20f;
    public bool isSprinting = false, isGrounded = false;
    public float sprintingMultiplier;
    public float movementSpeed = 1f;
    public float currentVelY = 0;



    public Transform groundCheck;
    public float groundDistance = 2f;
    public LayerMask groundMask;

    Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        Vector3 movement = new Vector3();

        movement = inputX * transform.right + inputY * transform.forward;
        if (isGrounded == false)
        {
            currentVelY += gravity * Time.deltaTime;
        }
        else if (isGrounded == true)
        {
            currentVelY = -2f;
        }


        if (isSprinting == true)
        {
            movement *= sprintingMultiplier;
        }

        controller.Move(movement * movementSpeed * Time.deltaTime);
        controller.Move(new Vector3(0, currentVelY * Time.deltaTime, 0));
    }
}



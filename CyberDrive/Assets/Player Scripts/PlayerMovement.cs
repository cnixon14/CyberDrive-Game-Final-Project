using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovement : MonoBehaviour
{
    // Controller class
    public CharacterController controller;
    public CapsuleCollider collider;
    public GameObject theCamera;
    public GameObject thePlayer;
    public Rigidbody rb;
    float origHeight;
    float redHeight = 1f;


    // Default physics parameters (adjustable)
    public float speed = 15f;
    public float knockbackSpeed = 500f;
    public float gravity = -15.00f;
    public float jumpHeight = 2.0f;
    public float fallSpeed = 0.0f;
    public float slideSpeed = 30f;
    public float airStompSpeed = 50f;
    // [SerializeField] [Range(0.0f, 0.5f)] 
    float smoothVar = 0.3f;

    // Gameplay parameters
    public bool isDouble = true;
    public float slideCool = 2f;
    public float controlCool = 20f;

    public Transform groundCheck; // Check class
    public float groundDistance = 0.4f; // Default distance to groundMask to be considered on the ground
    public LayerMask groundMask; // Mask utilized by isGrounded for ground check

    //Vector3 move;
    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVel = Vector2.zero;
    Vector2 inputDir = Vector2.zero;
    Vector3 velocity; // Player's current velocity and vector direction in the game space
    public bool isGrounded; // Is player grounded
    public bool isStomp; // Is player air stomping
    public bool isSlideIntoJump;
    public bool isSliding;
    public bool canControl;
    public static bool isDead;

    void Start()
    {
        GameObject thePlayerCylinder = GameObject.Find("PlayerCylinder");
        controller = GetComponent<CharacterController>();
        collider = thePlayerCylinder.GetComponent<CapsuleCollider>();
        origHeight = collider.height;

        theCamera = GameObject.Find("Main Camera");

        thePlayer = GameObject.Find("Player");

        canControl = true;
        isDead = false;
        

    }


    // Update is called once per frame
    void Update()
    {
        
        GroundChecking();

        if (Input.GetButtonDown("Jump") && isGrounded && !isSliding)
        {
            Jumping();
        }
        else if (Input.GetButtonDown("Jump") && !isGrounded && isDouble)
        {
            Jumping();
            isDouble = false;
        }
        else if(Input.GetButtonDown("Jump") && Input.GetButton("Slide") && isDouble)
        {
            Jumping();
            isSlideIntoJump = true;
        }

        if (Input.GetButtonDown("AirStomp") && !isStomp && !isGrounded)
        {
            //AirStomp();
            //isStomp = true;
        }

        if (isGrounded)
        {
            GroundMovement();
        } else
        {
            if (!isSlideIntoJump)
            {
                AirMovement(speed);
            } else
            {
                AirMovement(slideSpeed);
            }
            
        }
        if (Input.GetButtonUp("Slide"))
        {
            if (!isSlideIntoJump)
            {
                slideSpeed = 30f;
            }
            isSliding = false;
            slideCool = 0.5f;
            //collider.height = origHeight;
            controller.height = 4;
            theCamera.transform.position = new Vector3(thePlayer.transform.position.x, thePlayer.transform.position.y + 1.46f, thePlayer.transform.position.z);

        }
        if (slideCool > 0)
        {
            slideCool -= 1f * Time.deltaTime;
        }

        if (!canControl && !isDead && controlCool <= 0)
        {
            canControl = true;
            controlCool = 0.4f;
            velocity = (transform.forward * 0 + transform.right * 0) * 0;
        }


    }

    void GroundMovement()
    {
        // Get current x and z axis inputs
        //x = Input.GetAxis("Horizontal");
        //z = Input.GetAxis("Vertical");

        inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputDir.Normalize();
        currentDir = inputDir;

        // Create a vector update path based on x and z input
        //move = (transform.right * x) + (transform.forward * z);
        if (Input.GetButton("Slide") && Input.GetKey(KeyCode.W) && slideCool < 0)
        {
            isSliding = true;
            velocity = SlideMovement(inputDir);
            //collider.height = redHeight;
            controller.height = 3;
            theCamera.transform.position = new Vector3(thePlayer.transform.position.x, thePlayer.transform.position.y, thePlayer.transform.position.z);

        }
        else
        {
            velocity = (transform.forward * inputDir.y + transform.right * inputDir.x) * speed + Vector3.up * fallSpeed;
        }

        // Update player's location in the game space
        //controller.Move(move * (speed * Time.deltaTime));
        //controller.Move(Vector3.ClampMagnitude(move * Time.deltaTime, 1.0f) * speed);
        if (canControl)
        {
            controller.Move(velocity * Time.deltaTime);
        }
    }

    void AirMovement(float speed_var)
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            inputDir.Normalize();

            currentDir = Vector2.SmoothDamp(currentDir, inputDir, ref currentDirVel, smoothVar);
        }

        // Create a vector update path based on x and z input
        velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * speed_var + Vector3.up * fallSpeed;

        // Update player's location in the game space
        controller.Move(velocity * Time.deltaTime);
    }

    void GroundChecking()
    {
        // Check if Player is on the ground
        isGrounded = (Physics.CheckSphere(groundCheck.position, groundDistance, groundMask));
        
        if (isGrounded && fallSpeed < 0)
        {
            fallSpeed = -2f; // Used to keep player on the ground
        }
        if (fallSpeed < 100f)
        {
            fallSpeed += gravity * Time.deltaTime;
        }
        if (isGrounded)
        {
            if (isSlideIntoJump && !isSliding)
            {
                isSlideIntoJump = false;
            }
            isDouble = true;
            isStomp = false;
        }

    }
     void Jumping()
    {
        fallSpeed = Mathf.Sqrt(jumpHeight * -2f * gravity); // If conditions are met give player a positive y velocity
        //fallSpeed = jumpHeight;

        //velocity.y += gravity * Time.deltaTime; // Apply gravity to player 
        //controller.Move(velocity * Time.deltaTime); // Update player's location in the game space
    }

    Vector3 SlideMovement(Vector2 inputDir)
    {
        velocity = (transform.forward * inputDir.y) * slideSpeed + Vector3.up * fallSpeed;
        if (slideSpeed > 0)
        {
            slideSpeed -= 20f * Time.deltaTime;
        }
       
        return velocity;
    }

    void AirStomp()
    {
        fallSpeed -= airStompSpeed;
    }
}


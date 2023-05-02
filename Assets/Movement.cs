using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//added animation
public class Movement : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField]
    private Vector3 playerVelocity;
    [SerializeField]
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    [SerializeField]
    bool isJumping;
    [SerializeField]
    bool isFalling;
    [SerializeField]
    bool isGrounded;
    [SerializeField]
    private float lowMultiplier = 1.5f;
    [SerializeField]
    private float highMultiplier = 2.0f;
    [SerializeField]
    bool isPressingJump;
    [SerializeField]
    bool hasReleasedJump;

    [SerializeField]
    float? lastGroundedTime;
    [SerializeField]
    float graceTime = 0.5f;
    [SerializeField]
    float? lastJumpPressedTime;

    [SerializeField]
    private float rotationSpeed = 720;


    [SerializeField]
    private Transform cameraTransform;

    private Animator anim;

    private void Start()
    {
        controller = this.GetComponent<CharacterController>();
        Debug.Assert(controller != null, "Attach a Character Controller", this);

        anim = this.GetComponent<Animator>();
        Debug.Assert(anim != null, "Attach an Animator Controller", this);


        isJumping = !controller.isGrounded;
        isPressingJump = Input.GetButton("Jump");

    }

    void Update()
    {
        bool isWalking = false;
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer)
        {
            lastGroundedTime = Time.time;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // move where the camera is facing
        move = (Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * move);


        anim.SetFloat("movement", move.magnitude);
        

        if (move != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);

            float maxDegreesDelta = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, maxDegreesDelta);

            isWalking = true;
            anim.SetBool("isWalking", true);

        }
        else
        {
            isWalking = false;
            anim.SetBool("isWalking", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            lastJumpPressedTime = Time.time;
        }


        playerVelocity.y += MultiplyGravity(playerVelocity) * Time.deltaTime;
        if ( (Time.time - lastGroundedTime) < graceTime )
        {
            playerVelocity.y = -0.5f;
            anim.SetBool("isGrounded", true) ;
            isGrounded = true;
            anim.SetBool("isJumping", false);
            isJumping = false;
            anim.SetBool("isFalling", false);
            isFalling = false;

            controller.height = 2f;
        }
        else
        {
            anim.SetBool("isGrounded", false);
            isGrounded = false;

            if ( isJumping && playerVelocity.y < 0 || playerVelocity.y < -2)
            {
                anim.SetBool("isFalling", true);
            }
        }

        // Changes the height position of the player..
        if ((Time.time - lastGroundedTime) < graceTime & (Time.time - lastJumpPressedTime) < graceTime)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            isJumping = true;
            anim.SetBool("isJumping", true);
            controller.height = 1.3f;
            hasReleasedJump = false;
            lastGroundedTime = null;
            lastJumpPressedTime = null;
            
        }

        if (isJumping)
        {
            isPressingJump = Input.GetButton("Jump"); 
        }



        if (isGrounded == false)
        {
            controller.Move(move * playerSpeed * Time.deltaTime);
            controller.Move(playerVelocity * Time.deltaTime);
        }

    }

    private void OnAnimatorMove()
    {
        if (groundedPlayer||((Time.time - lastGroundedTime)< graceTime))
        {
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = playerVelocity.y * Time.deltaTime;
            controller.Move(deltaPosition);
        }
        
    } 

    private float MultiplyGravity(Vector3 playerVelocity)
    {
        if (isJumping & playerVelocity.y > 0 & (isPressingJump == false || hasReleasedJump == true))
        {
            hasReleasedJump = true;
            return gravityValue * lowMultiplier;
        }

        if (isJumping & playerVelocity.y < 0)
        {
            return gravityValue * highMultiplier;
        }

        return gravityValue;
    }
}
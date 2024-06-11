using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float jumpForce = 8f;
    public float jumpCooldown = 0.25f;
    public float dashCooldown = 0.25f;
    public float dashForce = 20f;
    public static float knockback = 10f;
    float knockBackCooldown = 0.25f;
    public static bool isKnockBacked;
    public static bool isKnockBackedHelper;
    bool readyToJump;
    public bool doubleJumpEnable;
    bool doubleJump;
    public bool dashEnable;
    public AudioClip jumpSFX;
    public Animator animator;
    bool readyToDash;
    bool dash;
    bool notDashing;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode shiftKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight = 2;
    public LayerMask groundLayer;
    public bool grounded;

    Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        doubleJump = true;
        orientation = gameObject.transform;
        readyToDash = true;
        dash = true;
        isKnockBacked = false;
        isKnockBackedHelper = false;
        animator = GameObject.FindGameObjectWithTag("Player Model").GetComponent<Animator>();
    }

    private void Update()
    {
        // ground check
        
        if (GameManager.gameEnded == false)
        {
            PlayerInput();
        }
        else 
        {
            rb.velocity = new Vector3(0,0,0);
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.gameEnded == false && readyToDash && !isKnockBacked)
        {
            MovePlayer();
        }
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput == 0 && verticalInput == 0) {
            animator.SetBool("isRunning", false);
        } else {
            animator.SetBool("isRunning", true);
        }


        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded && !isKnockBacked)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // double jump
        if(Input.GetKeyDown(jumpKey) && readyToJump && !grounded && doubleJump && doubleJumpEnable && !isKnockBacked)
        {
            readyToJump = false;
            doubleJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // dash
        if(Input.GetKeyDown(shiftKey) && readyToDash && dash && dashEnable && !isKnockBacked) 
        {
            readyToDash = false;
            dash = false;
            Dash();
            Invoke(nameof(ResetDash), dashCooldown);
        }

        if(grounded)
        {
            doubleJump = true;
            dash = true;
        }

        // variable height
        if(Input.GetKeyUp(jumpKey) && rb.velocity.y > 3f)
        {
            rb.velocity = new Vector3(rb.velocity.x, 2f, rb.velocity.z);
        }
        
        // Pogoing
        if (isKnockBackedHelper) 
            {
                Invoke(nameof(ResetKnockBack), knockBackCooldown);
                isKnockBackedHelper = false;
                dash = true;
                doubleJump = true;
            }

    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 40f, ForceMode.Force);

        rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
    }

    public static void KnockBack()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(-1 * Camera.main.transform.forward * knockback, ForceMode.Impulse);
        isKnockBackedHelper = true;
    }


    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        AudioSource.PlayClipAtPoint(jumpsSFX, transform.position);
        animator.SetTrigger("Jump");
    }

    private void Dash()
    {
        rb.velocity = new Vector3(0f, 0f, 0f);    
        rb.AddForce(Camera.main.transform.forward * dashForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void ResetDash()
    {
        readyToDash = true;  
        rb.velocity = new Vector3(0f, 0f, 0f);
    }

    private void ResetKnockBack()   
    {
        isKnockBacked = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        // Check if the player is on the ground
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f)
            {
                grounded = true;
                return;
            }
        }
        grounded = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }
}
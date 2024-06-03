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
    bool readyToJump;
    public bool doubleJumpEnable;
    bool doubleJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight = 2;
    public LayerMask groundLayer;
    public static bool grounded;

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
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, groundLayer);
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
        if (GameManager.gameEnded == false)
        {
            MovePlayer();
        }
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");


        // when to jump
        if(Input.GetKeyDown(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if(Input.GetKeyDown(jumpKey) && readyToJump && !grounded && doubleJump && doubleJumpEnable)
        {
            readyToJump = false;
            doubleJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if(grounded)
        {
            doubleJump = true;
        }

        // variable height
        if(Input.GetKeyUp(jumpKey) && rb.velocity.y > 3f)
        {
            rb.velocity = new Vector3(rb.velocity.x, 2f, rb.velocity.z);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 40f, ForceMode.Force);

        rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
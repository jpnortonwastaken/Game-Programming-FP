using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float acceleration = 10f;
    public float jumpForce = 22f;
    public float maxJumpHoldTime = 1.5f;
    public float jumpCooldown = 0.25f;
    public float dashCooldown = 0.5f;
    public float dashForce = 20f;
    public static float knockback = 10f;
    public static bool isKnockBacked;
    public static bool isKnockBackedHelper;
    bool readyToJump;
    public bool doubleJumpEnable = true;
    bool doubleJump;
    public bool dashEnable = true;
    public AudioClip jumpSFX;
    public Animator animator;
    bool readyToDash;
    public float gravityScale = 5f;

    KeyCode jumpKey = KeyCode.Space;
    KeyCode shiftKey = KeyCode.LeftShift;

    public float playerHeight = 2;
    public bool grounded;

    Transform cameraTransform;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;
    public float rotationSpeed = 10f;

    private float jumpHoldTime = 0f;
    private bool jumpKeyHeld = false;
    private bool isDashing = false;
    private bool hasDashed = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.drag = 5f;
        rb.mass = 1f;
        Physics.gravity *= gravityScale;

        readyToJump = true;
        doubleJump = true;
        readyToDash = true;
        isKnockBacked = false;
        isKnockBackedHelper = false;
        cameraTransform = Camera.main.transform;
        animator = GameObject.FindGameObjectWithTag("Player Model").GetComponent<Animator>();
    }

    private void Update()
    {
        if (!GameManager.gameEnded)
        {
            PlayerInput();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.gameEnded && readyToDash && !isKnockBacked)
        {
            MovePlayer();
        }
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        animator.SetBool("isRunning", horizontalInput != 0 || verticalInput != 0);

        if (Input.GetKeyDown(jumpKey))
        {
            jumpKeyHeld = true;
            if (readyToJump && grounded && !isKnockBacked)
            {
                readyToJump = false;
                jumpHoldTime = 0f;
                Jump();
            }
        }
        else if (Input.GetKeyUp(jumpKey))
        {
            jumpKeyHeld = false;
        }

        if (Input.GetKey(jumpKey) && !grounded && jumpHoldTime < maxJumpHoldTime)
        {
            jumpHoldTime += Time.deltaTime;
            rb.AddForce(Vector3.up * (jumpForce * Time.deltaTime), ForceMode.Impulse);
        }

        if (Input.GetKeyDown(jumpKey) && readyToJump && !grounded && doubleJump && doubleJumpEnable && !isKnockBacked)
        {
            readyToJump = false;
            doubleJump = false;
            Jump();
        }

        if (Input.GetKeyDown(shiftKey) && readyToDash && dashEnable && !isKnockBacked && !hasDashed)
        {
            readyToDash = false;
            Dash();
            Invoke(nameof(ResetDash), dashCooldown);
        }

        if (grounded)
        {
            doubleJump = true;
            hasDashed = false;
        }

        if (Input.GetKeyUp(jumpKey) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.5f, rb.velocity.z);
        }

        if (isKnockBackedHelper)
        {
            Invoke(nameof(ResetKnockBack), knockback);
            isKnockBackedHelper = false;
            doubleJump = true;
        }
    }

    private void MovePlayer()
    {
        if (isDashing) return;

        moveDirection = cameraTransform.forward * verticalInput + cameraTransform.right * horizontalInput;
        moveDirection.y = 0;

        Vector3 targetVelocity = moveDirection.normalized * moveSpeed;

        Vector3 velocity = Vector3.Lerp(rb.velocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public static void KnockBack()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(-Camera.main.transform.forward * knockback, ForceMode.Impulse);
        isKnockBackedHelper = true;
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        AudioSource.PlayClipAtPoint(jumpSFX, transform.position);
        animator.SetTrigger("Jump");
        Invoke(nameof(ResetJump), jumpCooldown);
    }

    private void Dash()
    {
        isDashing = true;
        hasDashed = true; // Set hasDashed to true
        rb.velocity = Vector3.zero;
        rb.AddForce(cameraTransform.forward * dashForce, ForceMode.Impulse);
        RotateToDashDirection();
    }

    private void RotateToDashDirection()
    {
        transform.rotation = Quaternion.LookRotation(cameraTransform.forward);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void ResetDash()
    {
        readyToDash = true;
        rb.velocity = Vector3.zero;
        isDashing = false;
    }

    private void ResetKnockBack()
    {
        isKnockBacked = false;
    }

    private void OnCollisionStay(Collision collision)
    {
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

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float speed = 5f;

    [Header("Jump")]
    public float jumpForce = 10f;
    public int maxJumps = 2;
    private int jumpsLeft;

    public float coyoteTime = 0.15f;
    private float coyoteCounter;

    public float jumpBufferTime = 0.15f;
    private float jumpBufferCounter;

    [Header("Dash")]
    public float dashForce = 18f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 0.5f;
    private bool isDashing;
    private bool canDash = true;

    [Header("Fast Fall")]
    public float fastFallSpeed = -20f;

    [Header("Wall System")]
    public float wallSlideSpeed = 2f;
    public float wallClimbSpeed = 3f;

    public Transform wallCheckLeft;
    public Transform wallCheckRight;
    public float wallCheckRadius = 0.2f;
    public LayerMask wallLayer;

    public float wallJumpForceX = 8f;
    public float wallJumpForceY = 10f;

    public float wallDetachTime = 0.2f;

    private bool isTouchingWall;
    private bool wallJumping;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsLeft = maxJumps;
    }

    void Update()
    {
        if (isDashing) return;

        GetInput();
        CheckSurroundings();
        HandleJumpBuffer();

        WallMovement();   // сначала стены
        Jump();           // потом обычный прыжок
        FastFall();
        Flip();

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (isDashing) return;
        Move();
    }

    void GetInput()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
    }

    void HandleJumpBuffer()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;
    }

    void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        bool touchLeft = Physics2D.OverlapCircle(wallCheckLeft.position, wallCheckRadius, wallLayer);
        bool touchRight = Physics2D.OverlapCircle(wallCheckRight.position, wallCheckRadius, wallLayer);

        isTouchingWall = touchLeft || touchRight;

        if (isGrounded)
        {
            jumpsLeft = maxJumps;
            coyoteCounter = coyoteTime;
            canDash = true;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }

    void Jump()
    {
        if (isTouchingWall && !isGrounded) return;

        if (jumpBufferCounter > 0 && (jumpsLeft > 0 || coyoteCounter > 0))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpBufferCounter = 0;
            jumpsLeft--;
        }
    }

    void FastFall()
    {
        if (!isGrounded && Input.GetKey(KeyCode.S))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fastFallSpeed);
        }
    }

    void WallMovement()
    {
        if (isTouchingWall && !isGrounded && !wallJumping)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                WallJump();
                return;
            }

            if (Input.GetKey(KeyCode.W))
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, wallClimbSpeed);
            }
            else if (rb.linearVelocity.y < -wallSlideSpeed)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -wallSlideSpeed);
            }
        }
    }

    void WallJump()
    {
        wallJumping = true;
        jumpBufferCounter = 0;
        jumpsLeft = maxJumps - 1;

        bool touchLeft = Physics2D.OverlapCircle(wallCheckLeft.position, wallCheckRadius, wallLayer);
        float jumpDirection = touchLeft ? 1f : -1f;

        rb.linearVelocity = new Vector2(jumpDirection * wallJumpForceX, wallJumpForceY);

        if ((jumpDirection > 0 && !facingRight) || (jumpDirection < 0 && facingRight))
        {
            facingRight = !facingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }

        StartCoroutine(StopWallJump());
    }

    IEnumerator StopWallJump()
    {
        yield return new WaitForSeconds(wallDetachTime);
        wallJumping = false;
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;

        float dashDirection = facingRight ? 1f : -1f;
        rb.linearVelocity = new Vector2(dashDirection * dashForce, 0f);

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    void Flip()
    {
        if (moveInput > 0 && !facingRight)
        {
            facingRight = true;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
        else if (moveInput < 0 && facingRight)
        {
            facingRight = false;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }

        if (wallCheckLeft != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(wallCheckLeft.position, wallCheckRadius);
        }

        if (wallCheckRight != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(wallCheckRight.position, wallCheckRadius);
        }
    }
}
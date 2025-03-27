using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour
{
    [Header("Physique aérienne")]
    [SerializeField] private float FallMultiplier = 2.5f;
    [SerializeField] private float LowJumpMultiplier = 2f;
    [SerializeField] private float AirControlFactor = 0.5f;


    [Header("Déplacement")]
    private float moveInput;
    public float jumpForce;
    public float moveSpeed;
    public float wallSlidingSpeed;

    [Header("Wall Jump")]
    public float wallJumpForce = 15f;
    public float wallJumpCooldown = 0.2f;
    private float wallJumpTimer = 0f;
    
    [Header("Dash")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    private bool hasDashed = false;


    [Header("Booléens")]
    public bool isGrounded;
    public bool isWalled;
    public bool isWallSliding;
    public bool isDashing;
    private bool facingRight = true;
    private bool hasWallJumped = false;

    [Header("Raycast")]
    private Vector2 PlayerPositionIsWalled;
    private Vector2 PlayerPositionIsGroudedLeft;
    private Vector2 PlayerPositionIsGroudedRight;
    public float offsetRaycast1 = 0f;
    public float offsetRaycast2 = 0f;

    public float CheckGroundDistance;
    public float CheckWallDistance;



    public Rigidbody2D rb;
    public LayerMask GroundLayer;
    

    void Update()
    {
        PlayerPositionIsWalled = new Vector2(transform.position.x, transform.position.y - 0.2f);
        PlayerPositionIsGroudedLeft = new Vector2(transform.position.x - offsetRaycast1, transform.position.y - 0.2f);
        PlayerPositionIsGroudedRight = new Vector2(transform.position.x + offsetRaycast2, transform.position.y - 0.2f);



        isGrounded = Physics2D.Raycast(PlayerPositionIsGroudedLeft, Vector2.down, CheckGroundDistance, GroundLayer) ||
                     Physics2D.Raycast(PlayerPositionIsGroudedRight, Vector2.down, CheckGroundDistance, GroundLayer);
        Debug.DrawRay(PlayerPositionIsGroudedLeft, Vector2.down * CheckGroundDistance, Color.blue);
        Debug.DrawRay(PlayerPositionIsGroudedRight, Vector2.down * CheckGroundDistance, Color.blue);

        isWalled = Physics2D.Raycast(PlayerPositionIsWalled, Vector2.right, CheckWallDistance, GroundLayer) ||
                   Physics2D.Raycast(PlayerPositionIsWalled, Vector2.left, CheckWallDistance, GroundLayer);
        Debug.DrawRay(PlayerPositionIsWalled, Vector2.right * CheckWallDistance, Color.red);
        Debug.DrawRay(PlayerPositionIsWalled, Vector2.left * CheckWallDistance, Color.red);

        if (isGrounded && !isWalled)
        {
            hasDashed = false;
        }

        if (wallJumpTimer > 0f)
        {
            wallJumpTimer -= Time.deltaTime;
        }

        if (!isWalled)
        {
            hasWallJumped = false;
        }

        MovePlayer();

        // Gestion du saut
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        GravityModifier();
        WallSlide();
        FlipCharacter();
        HandleDash();
        

    }


    private void MovePlayer()
    {
        moveInput = Input.GetAxis("Horizontal");

        if (!isDashing)
        {
            float controlFactor = isGrounded ? 1f : AirControlFactor; 
            rb.velocity = new Vector2(moveInput * moveSpeed * controlFactor, rb.velocity.y);
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            hasWallJumped = false;  
        }
        else if (isWalled && !isGrounded && !hasWallJumped)  
        {
            WallJump();
        }
    }

    void WallJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);

        float wallDirection = Mathf.Sign(transform.localScale.x);  

        rb.velocity = new Vector2(wallDirection * moveSpeed, wallJumpForce);

        hasWallJumped = true;

        wallJumpTimer = wallJumpCooldown;
    }

    void GravityModifier()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (FallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (LowJumpMultiplier - 1) * Time.deltaTime;
        }
    }


    private void WallSlide()
    {
        if (isWalled && !isGrounded && Mathf.Abs(moveInput) > 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !hasDashed)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        hasDashed = true;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;

        float dashX = (moveInput != 0) ? Mathf.Sign(moveInput) : 0; 
        float dashY = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) ? 1 :
                      (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) ? -1 : 0;

        if (dashX == 0 && dashY == 0)
        {
            dashX = facingRight ? 1 : -1;
        }

        Vector2 dashDirection = new Vector2(dashX, dashY).normalized;
        rb.velocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        isDashing = false;
    }
    private void FlipCharacter()
    {
        if (moveInput > 0 && !facingRight)
            Flip();
        else if (moveInput < 0 && facingRight)
            Flip();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
using UnityEngine;

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

    [Header("Booléens")]
    public bool isGrounded;
    public bool isWalled;
    public bool isWallSliding;

    [Header("Raycast")]
    public float CheckGroundDistance;
    public float CheckWallDistance;


    public Rigidbody2D rb;
    private Vector2 PlayerPosition;
    public LayerMask GroundLayer;


    void Update()
    {
        PlayerPosition = new Vector2 (transform.position.x, transform.position.y -0.2f);
        isGrounded = Physics2D.Raycast(PlayerPosition, Vector2.down, CheckGroundDistance, GroundLayer);
        Debug.DrawRay(PlayerPosition, Vector2.down * CheckGroundDistance, Color.red);
        isWalled = Physics2D.Raycast(PlayerPosition, Vector2.right, CheckWallDistance, GroundLayer) || 
                   Physics2D.Raycast(PlayerPosition, Vector2.left, CheckWallDistance, GroundLayer);
        Debug.DrawRay(PlayerPosition, Vector2.right * CheckWallDistance, Color.red);
        Debug.DrawRay(PlayerPosition, Vector2.left * CheckWallDistance, Color.red);

        MovePlayer();

        // Gestion du saut
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        GravityModifier();
        WallSlide();
    }

    void MovePlayer()
    {
        moveInput = Input.GetAxis("Horizontal");
        float controlFactor = isGrounded ? 1f : AirControlFactor; // Facteur de contrôle en l'air

        rb.velocity = new Vector2(moveInput * moveSpeed * controlFactor, rb.velocity.y);
    }

    void Jump()
    {
        if (isGrounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
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
}
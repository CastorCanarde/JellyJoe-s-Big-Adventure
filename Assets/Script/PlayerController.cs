using UnityEditor.Rendering.Universal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Physique aérienne")]
    [SerializeField] private float FallMultiplier = 2.5f;
    [SerializeField] private float LowJumpMultiplier = 2f;
    [SerializeField] private float AirControlFactor = 0.5f;


    public float jumpForce;
    public float moveSpeed;

    private bool isGrounded;

    public Rigidbody2D rb;
    private Vector2 _velocity = Vector2.zero;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;

    void Update()
    {
        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);

        MovePlayer();

        // Gestion du saut
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        GravityModifier();
    }

    void MovePlayer()
    {
        float moveInput = Input.GetAxis("Horizontal");
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
}
using UnityEngine;

public class VerticalRunnerController : MonoBehaviour
{
    public float verticalSpeed = 5f;
    public float horizontalSpeed = 5f;
    public float dashForce = 15f;
    public float dashCooldown = 1f;

    private Rigidbody2D rb;
    private bool isDashing = false;
    private float lastDashTime = -Mathf.Infinity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(rb.velocity.x, verticalSpeed);

        float moveX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveX * horizontalSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= lastDashTime + dashCooldown)
        {
            StartCoroutine(Dash(moveX));
        }
    }

    private System.Collections.IEnumerator Dash(float direction)
    {
        isDashing = true;
        lastDashTime = Time.time;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(direction * dashForce, rb.velocity.y);

        yield return new WaitForSeconds(0.2f);

        rb.gravityScale = originalGravity;
        isDashing = false;
    }
}
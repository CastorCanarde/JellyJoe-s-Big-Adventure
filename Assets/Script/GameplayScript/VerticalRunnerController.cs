using UnityEngine;
using System.Collections;

public class VerticalRunnerController : MonoBehaviour
{
    [Header("Movement")]
    public float verticalSpeed = 5f;
    public float horizontalSpeed = 5f;

    [Header("Dash")]
    public float dashForce = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private Rigidbody2D rb;
    private bool isDashing = false;
    private float lastDashTime = -Mathf.Infinity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // On désactive la gravité pour garder un mouvement constant vers le haut
    }

    void Update()
    {
        if (isDashing) return;

        // Mouvement vertical automatique
        Vector2 velocity = new Vector2(0, verticalSpeed);

        // Contrôle horizontal
        float moveX = Input.GetAxisRaw("Horizontal");
        velocity.x = moveX * horizontalSpeed;

        rb.velocity = velocity;

        // Dash (gauche ou droite avec Shift + direction)
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= lastDashTime + dashCooldown && moveX != 0)
        {
            StartCoroutine(Dash(moveX));
        }
    }

    IEnumerator Dash(float direction)
    {
        isDashing = true;
        lastDashTime = Time.time;

        // Appliquer le dash horizontal en conservant la montée
        rb.velocity = new Vector2(direction * dashForce, verticalSpeed);

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
    }
}
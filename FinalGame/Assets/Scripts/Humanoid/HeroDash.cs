using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDash : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float dashDistance = 40f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] float dashSpeed = 200f;
    [SerializeField] private float dashCooldown = 5f;
    
    [Header("Invincibility Settings")]
    [SerializeField] private float invincibilityDuration = 0.5f;
    [SerializeField] private int flashCount = 5;
    
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool canDash = true;
    private bool isInvincible = false;
    private bool isDashing = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (rb == null)
            Debug.LogError("Rigidbody2D component required for dash!");
        if (spriteRenderer == null)
            Debug.LogError("SpriteRenderer component required for visual effects!");
    }

    // Update is called once per frame
    void Update()
    {
        // Detect spacebar press for dash
        if (Input.GetKeyDown(KeyCode.Space) && canDash && !isDashing && rb != null)
        {
            StartCoroutine(PerformDash());
        }
    }
    
    private IEnumerator PerformDash()
    {
        canDash = false;
        isDashing = true;
        
        // Get dash direction
        Vector2 dashDirection = GetDashDirection();

        // Calculate dash speed
        // float dashSpeed = dashDistance / dashDuration;
        Vector2 originalVelocity = rb != null ? rb.velocity : Vector2.zero;

        // Apply dash velocity
        if (rb != null)
            rb.velocity = dashDirection * dashSpeed;

        // Start invincibility
        if (spriteRenderer != null)
            StartCoroutine(GrantInvincibility());

        Debug.Log("Dash started!");
        
        // Wait for dash duration
        yield return new WaitForSeconds(dashDuration);

        // Restore original velocity (stop dash movement)
        if (rb != null)
            rb.velocity = originalVelocity;
        isDashing = false;
        
        // Cooldown period
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        
        Debug.Log("Dash ready!");
    }
    
    private Vector2 GetDashDirection()
    {
        // Get input direction
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        Vector2 direction = new Vector2(horizontal, vertical).normalized;
        
        // If no input, dash forward based on facing direction
        if (direction == Vector2.zero)
        {
            direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        }
        
        return direction;
    }
    
    private IEnumerator GrantInvincibility()
    {
        isInvincible = true;
        
        // Start flashing effect
        if (spriteRenderer != null)
            StartCoroutine(FlashEffect());
        
        yield return new WaitForSeconds(invincibilityDuration);
        
        isInvincible = false;
        
        // Ensure sprite is fully visible
        if (spriteRenderer != null)
            spriteRenderer.color = Color.white;
    }
    
    private IEnumerator FlashEffect()
    {
        float flashInterval = invincibilityDuration / (flashCount * 2);
        
        for (int i = 0; i < flashCount; i++)
        {
            // Semi-transparent
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);
            yield return new WaitForSeconds(flashInterval);
            
            // Fully opaque
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(flashInterval);
        }
    }
    
    // Public methods for other scripts
    public bool IsInvincible() => isInvincible;
    public bool IsDashing() => isDashing;
    public bool CanDash() => canDash;
}

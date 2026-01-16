using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 7;

    private SpriteRenderer sprite;
    private Rigidbody2D body;

    private bool doubleJumpAvailable = true;
    private bool isGrounded = false;

    private Vector2 mousePosition;
    private Vector2 normalizedDirection;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.12f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Footsteps")]
    [SerializeField] private AudioSource footstepsSource;   // AudioSource mit deinem Geh-Clip (Loop an)
    [SerializeField] private float minRunSpeed = 0.1f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        if (footstepsSource == null)
            footstepsSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        UpdateGrounded();

        HandlePlayerRotation();
        HandlePlayerMovement();
        HandlePlayerJump();

        UpdateFootsteps();
    }

    private void UpdateGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // sobald du wirklich am Boden bist, Double Jump resetten
        if (isGrounded)
            doubleJumpAvailable = true;
    }

    private void HandlePlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0)
            body.linearVelocity = new Vector2(horizontalInput * movementSpeed, body.linearVelocity.y);
        else
            body.linearVelocity = new Vector2(0, body.linearVelocity.y);
    }

    private void HandlePlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || doubleJumpAvailable))
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, movementSpeed * 0.75f);

            if (!isGrounded)
                doubleJumpAvailable = false;
        }
    }

    private void UpdateFootsteps()
    {
        if (footstepsSource == null) return;

        float speedX = Mathf.Abs(body.linearVelocity.x);
        bool shouldPlay = isGrounded && speedX > minRunSpeed;

        if (shouldPlay)
        {
            if (!footstepsSource.isPlaying) footstepsSource.Play();
        }
        else
        {
            if (footstepsSource.isPlaying) footstepsSource.Stop();
        }
    }

    private void HandlePlayerRotation()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        normalizedDirection = (mousePosition - (Vector2)body.transform.position).normalized;

        if (normalizedDirection.x > 0) sprite.flipX = false;
        else if (normalizedDirection.x < 0) sprite.flipX = true;
    }

    // Optional: GroundCheck-Kreis im Scene-View sichtbar machen
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
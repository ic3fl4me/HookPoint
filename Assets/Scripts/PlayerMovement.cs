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

    [Header("Footsteps (Loop)")]
    [SerializeField] private AudioSource footstepsSource;   // AudioSource mit Geh-Loop (Loop an)
    [SerializeField] private float minRunSpeed = 0.1f;

    [Header("Jump Audio (OneShot)")]
    [SerializeField] private AudioSource sfxSource;         // SFX AudioSource (Loop aus)
    [SerializeField] private AudioClip jumpClip;            // Jump-Sound

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
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
        if (groundCheck == null) return;

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
            // Jump-Sound jedes Mal beim erfolgreichen Sprung (auch Double Jump)
            if (sfxSource != null && jumpClip != null)
                sfxSource.PlayOneShot(jumpClip, 1f);

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
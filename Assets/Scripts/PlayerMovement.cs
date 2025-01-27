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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HandlePlayerRotation();

        HandlePlayerMovement();

        HandlePlayerJump();
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            doubleJumpAvailable = true;
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void HandlePlayerRotation()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        normalizedDirection = (mousePosition - (Vector2)body.transform.position).normalized;

        if (normalizedDirection.x > 0)
        {
            sprite.flipX = false;
        }
        else if (normalizedDirection.x < 0) 
        {
            sprite.flipX = true;
        }
    }
}

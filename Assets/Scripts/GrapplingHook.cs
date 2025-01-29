using System;
using System.Net;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private float grappleLength = 3f;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LineRenderer grappleRope;

    private Vector2 grapplePoint;
    private DistanceJoint2D joint;
    private float hookRetractTimer;
    private bool hookIsAttached = false;
    private Vector2 mousePosition;
    private Vector2 playerPosition;
    private Vector2 direction;
    private RaycastHit2D hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Disable non-active grappling hook components
        joint = gameObject.GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        grappleRope.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate direction of mouse cursor in relation to player
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPosition = (Vector2)transform.position;
        direction = mousePosition - playerPosition;

        if (Input.GetMouseButtonDown(1))
        {
            // Send out a raycast to detect a hitbox (only in layer Ground) in direction of mouseCursor, up to the maximum grappling hook length
            hit = Physics2D.Raycast(
                origin: playerPosition,
                direction: direction.normalized,
                distance: grappleLength,
                layerMask: grappleLayer
                );

            // Check if ground was detected
            if (hit.collider != null)
            {
                HandleHookSurfaceHit();
            } else
            {
                HandleHookMiss();
            }
            DrawHook();
        }
        hookRetractTimer += Time.deltaTime;

        if (Input.GetMouseButtonUp(1) || (hookRetractTimer > 0.25 && !hookIsAttached))
        {
            // On release of right click hook is disabled
            joint.enabled = false;
            grappleRope.enabled = false;
            hookIsAttached = false;
        }

        UpdateHookOrigin();
    }

    private void HandleHookSurfaceHit()
    {
        // Set grappling hook attachment point to detected hit on ground
        grapplePoint = hit.point;
        joint.connectedAnchor = grapplePoint;
        joint.enabled = true;
        // Keep distance from point in time where hook connection occured
        joint.distance = (grapplePoint - playerPosition).magnitude;
        hookIsAttached = true;
    }

    private void HandleHookMiss()
    {
        // Stretch out hook if no surface was hit, up to maximum grappleLength
        if (direction.magnitude > grappleLength)
        {
            // Calculate point in direction of mouseCursor with max grappleLength
            grapplePoint = playerPosition + direction.normalized * grappleLength;
        }
        else
        {
            // Use mouseCursor position if it is within max allowed grappleLength
            grapplePoint = mousePosition;
        }
        hookIsAttached = false;
        // Value used to unrender hook after a certain time if nothing was hit
        hookRetractTimer = 0;
    }

    private void DrawHook()
    {
        // Points to render grapple rope
        grappleRope.SetPosition(0, grapplePoint);
        grappleRope.SetPosition(1, playerPosition);
        grappleRope.enabled = true;
    }

    private void UpdateHookOrigin()
    {
        // Keep hook origin at player position if player moves
        if (grappleRope.enabled)
        {
            grappleRope.SetPosition(1, playerPosition);
        }
    }
}
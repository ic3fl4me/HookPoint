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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        joint = gameObject.GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        grappleRope.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        playerPosition = (Vector2)transform.position;
        direction = (mousePosition - playerPosition);

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(
                origin: playerPosition,
                direction: direction.normalized,
                distance: grappleLength,
                layerMask: grappleLayer
                );

            if (hit.collider != null)
            {
                grapplePoint = hit.point;
                joint.connectedAnchor = grapplePoint;
                joint.enabled = true;
                joint.distance = (grapplePoint - playerPosition).magnitude;
                hookIsAttached = true;
            } else
            {
                // Stretch out hook if no surface was hit, up to maximum grappleLength
                if (direction.magnitude > grappleLength)
                {
                    // Calculate point in direction of mouseCursor with max grappleLength
                    grapplePoint = playerPosition + direction.normalized * grappleLength;
                } else
                {
                    // Use mouseCursor position if it is within max allowed grappleLength
                    grapplePoint = mousePosition;
                }
                hookIsAttached = false;
                hookRetractTimer = 0;
            }
            grappleRope.SetPosition(0, grapplePoint);
            grappleRope.SetPosition(1, playerPosition);
            grappleRope.enabled = true;
        }
        hookRetractTimer += Time.deltaTime;

        if (Input.GetMouseButtonUp(1) || (hookRetractTimer > 0.25 && !hookIsAttached))
        {
            joint.enabled = false;
            grappleRope.enabled = false;
            hookIsAttached = false;
        }

        UpdateHookOrigin();
    }

    private void UpdateHookOrigin()
    {
        if (grappleRope.enabled)
        {
            grappleRope.SetPosition(1, playerPosition);
        }
    }
}
using System;
using System.Net;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private float grappleLength = 10f;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LineRenderer grappleChain;

    private Vector3 grapplePoint;
    private DistanceJoint2D joint;
    private float hookRetractTimer;
    private bool hookIsAttached = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        joint = gameObject.GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        grappleChain.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Linecast(
                start: grapplePoint,
                end: grapplePoint
                );

            if (hit.collider != null)
            {
                grapplePoint = hit.point;
                joint.connectedAnchor = grapplePoint;
                joint.enabled = true;
                joint.distance = grappleLength;
                hookIsAttached = true;
            } else
            {
                grapplePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                hookIsAttached = false;
                hookRetractTimer = 0;
            }
            grappleChain.SetPosition(0, grapplePoint);
            grappleChain.SetPosition(1, transform.position);
            grappleChain.enabled = true;
        }
        hookRetractTimer += Time.deltaTime;

        if (Input.GetMouseButtonUp(1) || (hookRetractTimer > 0.25 && !hookIsAttached))
        {
            joint.enabled = false;
            grappleChain.enabled = false;
            hookIsAttached = false;
        }

        UpdateHookOrigin();
    }

    private void UpdateHookOrigin()
    {
        if (grappleChain.enabled)
        {
            grappleChain.SetPosition(1, transform.position);
        }
    }
}

/* Code to fall back to if grappling hook changes do not work
 * 
 * public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private float grappleLength = 10f;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LineRenderer grappleChain;

    private Vector3 grapplePoint;
    private DistanceJoint2D joint;
    private float hookRetractTimer;
    private bool hookIsAttached = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        joint = gameObject.GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        grappleChain.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(
                origin: Camera.main.ScreenToWorldPoint(Input.mousePosition),
                direction: Vector2.zero,
                distance: grappleLength,
                layerMask: grappleLayer
                );

            if (hit.collider != null)
            {
                grapplePoint = hit.point;
                joint.connectedAnchor = grapplePoint;
                joint.enabled = true;
                joint.distance = grappleLength;
                hookIsAttached = true;
            } else
            {
                grapplePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                hookIsAttached = false;
                hookRetractTimer = 0;
            }
            grappleChain.SetPosition(0, grapplePoint);
            grappleChain.SetPosition(1, transform.position);
            grappleChain.enabled = true;
        }
        hookRetractTimer += Time.deltaTime;

        if (Input.GetMouseButtonUp(1) || (hookRetractTimer > 0.25 && !hookIsAttached))
        {
            joint.enabled = false;
            grappleChain.enabled = false;
            hookIsAttached = false;
        }

        UpdateHookOrigin();
    }

    private void UpdateHookOrigin()
    {
        if (grappleChain.enabled)
        {
            grappleChain.SetPosition(1, transform.position);
        }
    }
}*/
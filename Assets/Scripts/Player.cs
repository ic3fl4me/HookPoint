using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        // Lock camera to player (slightly above player)
        cam.transform.position = new Vector3(transform.position.x, transform.position.y + 1, -10);
    }
}

using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private Camera cam;
    private Vector2 spawnPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        spawnPoint = (Vector2)transform.position;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // Lock camera to player
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        HandlePlayerRespawn();
    }

    private void HandlePlayerRespawn()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = spawnPoint;
            this.currentHealth = this.maxHealth;
            fellInVoid = false;
            sprite.enabled = true;
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            body.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}

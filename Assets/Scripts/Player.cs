using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private Camera cam;
    private Vector2 spawnPoint;
    private GameObject playerInstance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        spawnPoint = (Vector2)transform.position;
        playerInstance = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Lock camera to player
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        HandlePlayerRespawn();
    }

    private void HandlePlayerRespawn()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Destroy(playerInstance);
            Instantiate(playerInstance, new Vector2(0, 0), Quaternion.identity);
        }
    }
}

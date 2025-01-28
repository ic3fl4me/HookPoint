using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float attackCooldown = 1;

    private Vector2 mousePosition;
    private Vector2 normalizedDirection;
    private GameObject bulletInstance;
    private float cooldownTimer = Mathf.Infinity;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleGunRotation();
        
        // Left click to shoot, with predetermined cooldown
        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown)
            Attack();
        cooldownTimer += Time.deltaTime;
    }

    private void HandleGunRotation()
    {
        // Calculate position of mouse cursor in relation to player
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        normalizedDirection = (mousePosition - (Vector2)gun.transform.position).normalized;
        // Rotate gun
        gun.transform.right = normalizedDirection;
        // Bullet is spawned with right side of sprite facing outwards from the gun
        bulletSpawnPoint.transform.right = normalizedDirection;
    }

    private void Attack()
    {
        // New bullet is created
        bulletInstance = Instantiate(bullet, bulletSpawnPoint.position, gun.transform.rotation);

        cooldownTimer = 0;
    }
}

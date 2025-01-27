using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float attackCooldown = 1;
    private Vector2 worldPosition;
    private Vector2 direction;
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

        if (gameObject.activeSelf && Input.GetKeyDown(KeyCode.Mouse0) && cooldownTimer > attackCooldown)
            Attack();
        cooldownTimer += Time.deltaTime;
    }

    private void HandleGunRotation()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (worldPosition - (Vector2)gun.transform.position).normalized;
        gun.transform.right = direction;
        bulletSpawnPoint.transform.right = direction;
    }

    private void Attack()
    {
        bulletInstance = Instantiate(bullet, bulletSpawnPoint.position, gun.transform.rotation);

        cooldownTimer = 0;
    }
}

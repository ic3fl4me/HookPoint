using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float attackCooldown = 1;
    [SerializeField] private int clipSize = 3;
    [SerializeField] private int ammo;
    [SerializeField] private float reloadTime = 2f;

    private Vector2 mousePosition;
    private Vector2 normalizedDirection;
    private GameObject bulletInstance;
    private float cooldownTimer = Mathf.Infinity;
    private bool gunActive = true;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ammo = clipSize;
    }

    // Update is called once per frame
    void Update()
    {
        HandleGunRotation();

        if (ammo <= 0)
        {
            StartCoroutine(Reload());
        }
        
        // Left click to shoot, with predetermined cooldown
        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && gunActive && ammo > 0)
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
        ammo--;
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        ammo = clipSize;
    }

    public void EnableGun()
    {
        gunActive = true;
    }

    public void DisableGun()
    {
        gunActive = false;
    }
}

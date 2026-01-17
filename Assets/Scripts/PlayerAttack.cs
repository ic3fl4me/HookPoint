// PlayerAttack.cs (mit Raketen/Schuss-Sound + Reload-Fix)
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

    [Header("Audio (SFX)")]
    [SerializeField] private AudioSource sfxSource;  // eigener AudioSource für Raketen/SFX
    [SerializeField] private AudioClip rocketClip;   //  Raketen/Schuss-Clip
    [SerializeField] private AudioClip reloadClip;      // Nachladen
    [SerializeField, Range(0f, 1f)] private float rocketVolume = 1f;
    [SerializeField, Range(0f, 1f)] private float reloadVolume = 1f;

    private Vector2 mousePosition;
    private Vector2 normalizedDirection;
    private GameObject bulletInstance;
    private float cooldownTimer = Mathf.Infinity;
    private bool gunActive = true;
    private bool isReloading = false;

    void Start()
    {
        ammo = clipSize;
    }

    void Update()
    {
        HandleGunRotation();

        if (ammo <= 0 && !isReloading)
            StartCoroutine(Reload());

        // Linksklick schießen
        if (Input.GetMouseButtonDown(0) &&
            cooldownTimer > attackCooldown &&
            gunActive &&
            ammo > 0 &&
            !isReloading)
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void HandleGunRotation()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        normalizedDirection = (mousePosition - (Vector2)gun.transform.position).normalized;

        gun.transform.right = normalizedDirection;
        bulletSpawnPoint.transform.right = normalizedDirection;
    }

    private void Attack()
    {
        bulletInstance = Instantiate(bullet, bulletSpawnPoint.position, gun.transform.rotation);

        // Raketen/Schuss-Sound
        if (sfxSource != null && rocketClip != null)
            sfxSource.PlayOneShot(rocketClip, 1f);

        cooldownTimer = 0;
        ammo--;
    }

    private IEnumerator Reload()
    {
        isReloading = true;

        // Reload-Sound (einmal beim Start)
        if (sfxSource != null && reloadClip != null)
            sfxSource.PlayOneShot(reloadClip, reloadVolume);

        yield return new WaitForSeconds(reloadTime);
        ammo = clipSize;
        isReloading = false;
    }

    public void EnableGun() => gunActive = true;
    public void DisableGun() => gunActive = false;
}
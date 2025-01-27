using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float playerHealth = 1;
    [SerializeField] private Animator animator;
    [SerializeField] private Camera cam;
    private Rigidbody2D body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = new Vector3(transform.position.x, transform.position.y + 1, -10);
    }
    public void Damage(float damageAmount)
    {
        playerHealth -= damageAmount;

        if (playerHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("PlayerDeath");
        body.constraints = RigidbodyConstraints2D.FreezeAll;
        gameObject.SetActive(false);
    }
}

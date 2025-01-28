using System;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 1;
    [SerializeField] private float currentHealth;
    [SerializeField] protected Rigidbody2D body;
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer renderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger(gameObject.name + "Death");
        renderer.enabled = false;

    }
}

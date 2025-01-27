using System;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 1;
    [SerializeField] private float currentHealth;
    [SerializeField] protected Rigidbody2D body;
    [SerializeField] protected Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        body = GetComponent<Rigidbody2D>();

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
        body.constraints = RigidbodyConstraints2D.FreezeAll;
        gameObject.SetActive(false);
        animator.SetTrigger(gameObject.name + "Death");
    }
}

using System;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 1;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
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

using System;
using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 1;
    [SerializeField] private float currentHealth;
    [SerializeField] protected Rigidbody2D body;
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

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
            StartCoroutine("Die");
        }
    }

    private IEnumerator Die()
    {
        body.constraints = RigidbodyConstraints2D.FreezeAll;

        if (gameObject.name == "Player")
        {
            animator.SetTrigger("PlayerDeath");
            yield return new WaitForSeconds(0.1f);
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
            sprite.enabled = false;
        } else
        {
            this.gameObject.SetActive(false);
        }
    }
}

using System;
using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    [SerializeField] protected float maxHealth = 1;
    [SerializeField] protected float currentHealth;
    [SerializeField] protected Rigidbody2D body;
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer sprite;

    protected bool fellInVoid = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Falling into the void instantly kills
        if (transform.position.y < -12 && !fellInVoid)
        {
            this.Damage(999);
            fellInVoid = true;
        }
    }

    // Default method for all enities to take damage, and call Die if currentHealth falls to 0
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

        // If object is a player, sprite is not rendered, otherwise objects like enemies are deactivated if they die
        if (gameObject.name == "Player")
        {
            animator.SetTrigger("PlayerDeath");
            // Give animation time to play
            yield return new WaitForSeconds(0.1f);
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
            sprite.enabled = false;
        } else
        {
            this.gameObject.SetActive(false);
        }
    }
}

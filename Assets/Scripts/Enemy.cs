using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private float contactDamage = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            IDamageable iDamageable = collision.gameObject.GetComponent<IDamageable>();
            iDamageable.Damage(contactDamage);
        }
    }
}

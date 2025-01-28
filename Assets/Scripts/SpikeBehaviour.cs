using UnityEngine;

public class SpikeBehaviour : MonoBehaviour
{
    [SerializeField] private float contactDamage = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Only damage players
        if (collision.gameObject.name == "Player")
        {
            IDamageable iDamageable = collision.gameObject.GetComponent<IDamageable>();
            iDamageable.Damage(contactDamage);
        }
    }
}

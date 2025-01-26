using System;
using UnityEngine;

public class BulletExplosionBehaviour : MonoBehaviour
{
    [SerializeField] private float destroyTime = 1f;
    [SerializeField] private float explosionDamage = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetDestroyTime();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetDestroyTime()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable iDamageable = collision.gameObject.GetComponent<IDamageable>();
        if (iDamageable != null)
        {
            iDamageable.Damage(explosionDamage);
        }
    }
}

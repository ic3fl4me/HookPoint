using System;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float destroyTime = 5f;
    [SerializeField] private LayerMask bulletCollisionObjects;
    [SerializeField] private GameObject explosion;
    private Rigidbody2D body;
    private GameObject explosionInstance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        SetStraightVelocity();

        SetDestroyTime();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetStraightVelocity()
    {
        body.linearVelocity = transform.right * bulletSpeed;
    }

    private void SetDestroyTime()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((bulletCollisionObjects.value & (1 << collision.gameObject.layer)) > 0)
        {
            explosionInstance = Instantiate(explosion, body.position, new Quaternion());

            Destroy(gameObject);
        }
    }
}

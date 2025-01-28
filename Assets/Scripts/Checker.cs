using UnityEngine;

public class Checker : MonoBehaviour
{
    private Patrol patrol;

    private void Start()
    {
        patrol = GetComponentInParent<Patrol>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
        {
            patrol.Flip();
        }
    }
}

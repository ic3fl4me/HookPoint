using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D myRigidBody;
    BoxCollider2D myBoyCollider;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myBoyCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(IsFacingRight())
        {
            myRigidBody.linearVelocity = new Vector2(moveSpeed, 0f);
        }
        else 
        {
            myRigidBody.linearVelocity = new Vector2(-moveSpeed, 0f);
        }

    }
    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-Mathf.Sign(myRigidBody.angularVelocity), transform.localScale.y);
    }
}



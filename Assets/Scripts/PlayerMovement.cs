using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3;
    private Rigidbody2D body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            body.linearVelocity = new Vector3(-movementSpeed, body.linearVelocity.y, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            body.linearVelocity = new Vector3(movementSpeed, body.linearVelocity.y, 0);
        }
        else
        {
            body.linearVelocity = new Vector3(0, body.linearVelocity.y, 0);
        }
        
        if (Input.GetKey(KeyCode.Space)){
            body.linearVelocity = new Vector3(body.linearVelocity.x, movementSpeed, 0);
        }
    }
}

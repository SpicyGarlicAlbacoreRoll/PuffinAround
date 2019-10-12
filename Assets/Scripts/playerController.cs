using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D playerRB;
    public LayerMask groundLayer;

    public Vector2 position;
    public Vector2 velocity;
    public Vector2 acceleration;
    public float gravity = 9.8f;
    public float moveSpeed = 2.0f;
    public float jumpSpeed = 2.0f;
    void Start()
    {
        // playerRB = gameObject.GetComponent<Rigidbody2D>();
        position = transform.position;
        acceleration.y = -gravity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 intialVelocity = velocity;
        // position = transform.position;

        Vector2 xAxisMovement = new Vector2(Input.GetAxisRaw("Horizontal"), 0.0f);

        if(!IsGrounded()) {
            Falling();
        }

        Move();
        Jump();
        transform.position = (velocity * Time.deltaTime)  + (0.5f * acceleration * Time.deltaTime * Time.deltaTime);
        position = transform.position;
    }

    void Move() {
        Vector2 direction = Input.GetAxisRaw("Horizontal") * Vector2.right;
        Vector2 targetVelocity = direction * moveSpeed;
        velocity += targetVelocity;
        // position += (direction * Time.deltaTime * moveSpeed);

        // transform.position = position;
    }

    bool IsGrounded() {
        Vector2 direction = Vector2.down;
        // Vector2 position = transform.position;

        float distance = 0.175f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if(hit.collider != null) {
            Debug.Log("GROUNDED");
            // velocity.y = 0;
            // acceleration.y = 0;
            return true;
            
        }

        return false;
    }

    void Falling() {
        acceleration.y = -gravity;
        // position += (Vector2.up * acceleration.y * Time.deltaTime);
        // velocity.y -= gravity * Time.deltaTime;
        // acceleration.y = -gravity;
        velocity.y -= gravity * Time.deltaTime;
        // acceleration.y = -gravity;
        // transform.position = position;
    }

    void Jump() {
        if (Input.GetAxisRaw("Jump") == 1 && IsGrounded()) {
            // acceleration.y = gravity;
            Vector2 jumpVec = Vector2.up * jumpSpeed * Time.deltaTime;
            // position += jumpVec;
            // transform.position = position;
            // velocity.y += 5000.0f * Time.deltaTime;
            velocity.y = 10.0f;

        }
    }
}

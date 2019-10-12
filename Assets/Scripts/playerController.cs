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
    void Start()
    {
        // playerRB = gameObject.GetComponent<Rigidbody2D>();
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        Vector2 xAxisMovement = new Vector2(Input.GetAxisRaw("Horizontal"), 0.0f);
        // playerRB.AddForce(xAxisMovement);

        if(!IsGrounded()) {
            Falling();
        }

        Move();
    }

    void Move() {
        Vector2 direction = Input.GetAxisRaw("Horizontal") * Vector2.right;
        position += (direction * Time.deltaTime * 2.0f);
        transform.position = position;
    }

    bool IsGrounded() {
        Vector2 direction = Vector2.down;
        Vector2 position = transform.position;

        float distance = 1.0f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if(hit.collider != null) {
            return true;
        }

        return false;
    }

    void Falling() {
        position -= (Vector2.up * gravity * Time.deltaTime);
        transform.position = position;
    }
}

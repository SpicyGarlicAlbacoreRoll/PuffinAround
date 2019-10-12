using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D playerRB;
    public LayerMask groundLayer;
    
    public GameObject projectile;

    public Vector2 position;
    public Vector2 velocity;
    public Vector2 acceleration;
    public float gravity = 9.8f;

    public float fireCooldown = 0f;
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

        if(Input.GetKey(KeyCode.F))
        {
            if (fireCooldown <= 0f) {
                position = transform.position;
                Instantiate(projectile, transform);
                fireCooldown = 0.5f;
            }
        }

        if(fireCooldown > 0f) {
            fireCooldown -= Time.deltaTime;
        }

        Jump();
        Vector2 targetTranslation = (velocity * Time.deltaTime)  + (0.5f * acceleration * Time.deltaTime * Time.deltaTime);
        // transform.Translate(targetTranslation);
        transform.position = Vector2.Lerp(transform.position, targetTranslation, 0.15f);
        position = transform.position;
    }

    void Move() {
        Vector2 direction = Input.GetAxisRaw("Horizontal") * Vector2.right;
        Vector2 targetVelocity = direction * moveSpeed;
        velocity += targetVelocity;
        // position += (direction * Time.deltaTime * moveSpeed);

        // transform.position = position;
    }

    void LaunchProjctile() {

    }

    bool IsGrounded() {
        Vector2 direction = Vector2.down;
        // Vector2 position = transform.position;

        float distance = 0.275f;

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
            velocity.y = 20.0f;

        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "waterTile") {
            gravity = 4.45f;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.name == "waterTile") {
            gravity = 9.8f;
        }    
    }
}

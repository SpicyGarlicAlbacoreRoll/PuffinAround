using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D playerRB;
    public LayerMask groundLayer;
    
    public GameObject projectile;

    public Vector2 position;
    public Vector2 velocity;
    public Vector2 acceleration;
    public float gravity = 75.0f;

    public float fireCooldown = 0f;
    public float moveSpeed = 2.0f;
    public float jumpSpeed = 2.0f;

    public bool isInWater = false;
    public bool isOnSpike = false;
    public int playerDirection;
    // public int featherCounter = 0;

    GameObject[] Feathers;
    public int health = 5;

    public Text featherText;
    public Text healthText;
    public int beakCounter = 0;
    public float playerEnemeyThreshold = 0.05f;

    public int maxJumps = 1;
    public int jumpCount = 0;
    public float jumpCoolDown = 0.3f;
    public float jumpCoolDownTimer = 0.0f;
    public int featherCounter = 0;

    private SpriteRenderer playerSprite;
    void Start()
    {
        position = transform.position;
        acceleration.y = -gravity;
        playerSprite = GetComponent<SpriteRenderer>();
        featherText = GameObject.Find("FeatherText").GetComponent<Text>();
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        position = transform.position;
        
        Vector2 intialVelocity = velocity;
        // position = transform.position;

        Vector2 xAxisMovement = new Vector2(Input.GetAxisRaw("Horizontal"), 0.0f);

        if(!IsGrounded()) {
            Falling();
        }

        Move();

        if(Input.GetKeyDown(KeyCode.F) && fireCooldown <= 0.45f) {
            FireProjectile();
        }

        else if(Input.GetKey(KeyCode.F) && fireCooldown <= 0f)
        {
            FireProjectile();
        }

        if(fireCooldown > 0f) {
            fireCooldown -= Time.deltaTime;
        }

        Jump();
        Vector2 targetTranslation = (velocity * Time.deltaTime)  + (0.5f * acceleration * Time.deltaTime * Time.deltaTime);
        // transform.Translate(targetTranslation);
        transform.position = Vector2.Lerp(transform.position, targetTranslation, 0.15f);
        if(jumpCoolDownTimer < jumpCoolDown) {
            jumpCoolDownTimer += Time.deltaTime;
        }
        healthText.GetComponent<UnityEngine.UI.Text>().text = "Health: " + health.ToString();
        featherText.GetComponent<UnityEngine.UI.Text>().text = "Feathers: " + featherCounter.ToString();
    }

    void Move() {
        if(Input.GetAxisRaw("Horizontal") != 0) {
            if(!collidingWall(Input.GetAxisRaw("Horizontal"))) {
                Vector2 direction = Input.GetAxisRaw("Horizontal") * Vector2.right;
                setDirection(Input.GetAxisRaw("Horizontal"));
                Vector2 targetVelocity = direction * moveSpeed;
                velocity += targetVelocity;
            }
        }

        // position += (direction * Time.deltaTime * moveSpeed);

        // transform.position = position;
    }

    void FireProjectile() {
        Instantiate(projectile, transform.position, transform.rotation);
        fireCooldown = 0.5f;
    }

    bool IsGrounded() {
        Vector2 direction = Vector2.down;
        // Vector2 position = transform.position;

        float distance = 1.5f;

        Debug.DrawRay(position - new Vector2(0, 0.1f), direction, Color.yellow);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if(hit.collider != null) {
            if (hit.collider.gameObject.tag == "hazard" && !isOnSpike) {
                print("hit spike");
                HitSpike();
            }
            if (hit.collider.gameObject.tag == "enemy") {
                print("enemy hit");
                Destroy(hit.collider.gameObject);
            }
            isOnSpike = (hit.collider.gameObject.tag == "hazard");
            // velocity.y = 0;
            // acceleration.y = 0;
            transform.position = position;
            jumpCount = 0;
            return true;
            
        }

        return false;
    }

    bool collidingWall(float velDirection) {
            Vector2 direction = new Vector2(velDirection, 0);
        // Vector2 position = transform.position;

        float distance = 1.5f*0.35f;

        //offset is for measure from the bottom corner of the player
        Debug.DrawRay(position - new Vector2(0, 0.1f), direction, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(position - new Vector2(0, 0.1f), direction, distance, groundLayer);
        if(hit.collider != null) {
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
        velocity.y -= gravity * Time.deltaTime * 5.0f;
        // acceleration.y = -gravity;
        // transform.position = position;
    }

    void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCoolDownTimer >= jumpCoolDown && (IsGrounded() || isInWater || jumpCount < maxJumps)) {
            // acceleration.y = gravity;
            Vector2 jumpVec = Vector2.up * jumpSpeed * Time.deltaTime;
            // position += jumpVec;
            // transform.position = position;
            // velocity.y += 5000.0f * Time.deltaTime;
            if(isInWater) {
                velocity.y += 30.0f;
            }
            else {
                if(!isInWater) {
                    jumpCount++;
                }

                velocity.y += 160.0f;
                jumpCoolDownTimer = 0.0f;
            }
                

        }
    }

    void HitSpike() {
        TakeDamage();
    }

    void AddFeather() {
        featherCounter++;
        maxJumps = (featherCounter / 3) + 1;         
    }

    void TakeDamage() {
        health--;
        if (health == 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public int getDirection() {
        
        return playerDirection;
    }

    public void setDirection(float direction) {
        if(direction < 0) {
            
            playerDirection = -1;
            playerSprite.flipX = true;

        } else {
            playerSprite.flipX = false;
            playerDirection = 1;
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "water") {
            gravity = 25f;
            isInWater = true;
            jumpCount = 0;
        } else if(other.gameObject.tag == "feather") {
            AddFeather();
        } else if(other.gameObject.tag == "enemy" || other.gameObject.tag == "enemy_jump") {
            float playerLowerBound = gameObject.GetComponent<Collider2D>().bounds.min.y;
            float enemyUpperBound = other.gameObject.GetComponent<Collider2D>().bounds.max.y;
            float difference = Mathf.Abs(playerLowerBound - enemyUpperBound);

            if (difference < playerEnemeyThreshold) {
                Destroy(other.gameObject.transform.parent.gameObject);
            } else { 
                TakeDamage(); 
            }
        } else if(other.gameObject.tag == "beak") {
            beakCounter++;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "water") {
            gravity = 75f;
            isInWater = false;
        }    
    }
}

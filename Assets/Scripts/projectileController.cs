using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileController : MonoBehaviour
{
    float viewThreshold = 1f;
    Rigidbody2D projectileRB;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        projectileRB = gameObject.GetComponent<Rigidbody2D>();
        // projectileRB = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        if (player == null) {
            Debug.Log("Can't find game object with tag");
        }
        playerController playerScript = player.GetComponent<playerController>();
        int direction = playerScript.getDirection();
        Debug.Log($"FIRING IN {direction}");
        projectileRB.AddForce(transform.right * 100f * direction);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

        if (viewPos.x > viewThreshold) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "enemy") {
            Destroy(gameObject);
        }
    }
}

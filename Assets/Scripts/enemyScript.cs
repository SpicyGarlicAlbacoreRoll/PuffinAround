using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public GameObject player;

    float speed = 2f;
    Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("pufffinPlaceholder");

        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 enemyPos = transform.position;
        float distanceToPlayer = Vector3.Distance(playerPos, enemyPos);

        transform.Translate(speed * Time.deltaTime, 0,0);

        if (Mathf.Abs(originalPos.x - transform.position.x) > 2.0f) {
            speed *= -1.0f;
        }
        if(speed < 0) {
            GetComponent<SpriteRenderer>().flipX = false;
        } else {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject != player &&
            other.contacts[0].normal.y != 1.0f) {
            speed *= -1.0f;
        } 
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "bullet") {
            Destroy(gameObject);
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("pufffinPlaceholder");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 enemyPos = transform.position;
        float distanceX = playerPos.x - enemyPos.x;
        float distanceY = playerPos.y - enemyPos.y;
        float distanceToPlayer = Mathf.Pow(distanceX * distanceX + distanceY * distanceY, 0.5f);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "bullet") {
            Destroy(gameObject);
        }
    }
}

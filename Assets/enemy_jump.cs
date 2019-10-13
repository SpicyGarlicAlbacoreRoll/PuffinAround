using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_jump : MonoBehaviour
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
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject == player) {
            print("player hit");
        //&& other.contacts[0].normal.y == -1.0f) {
            Destroy(gameObject);
        }
    }
}

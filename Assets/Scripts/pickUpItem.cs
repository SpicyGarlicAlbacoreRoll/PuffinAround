using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpItem : MonoBehaviour
{
    private void Start() {
        GetComponent<AudioSource>().playOnAwake = false; 
    }
    //If the player touches the item, the item destroys itself
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") 
        {
            GetComponent<AudioSource>().Play();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameObject.Destroy(gameObject, 1.5f);
        }
    }
        
    
}

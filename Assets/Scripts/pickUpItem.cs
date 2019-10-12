using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpItem : MonoBehaviour
{

    //If the player touches the item, the item destroys itself
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") 
        {
            GameObject.Destroy(gameObject, 0.15f);
        }
    }
        
    
}

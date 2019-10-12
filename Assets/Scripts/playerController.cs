using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D playerRB;
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 xAxisMovement = new Vector2(Input.GetAxisRaw("Horizontal"), 0.0f);
        playerRB.AddForce(xAxisMovement);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileController : MonoBehaviour
{
    Rigidbody2D projectileRB;
    // Start is called before the first frame update
    void Start()
    {
        projectileRB = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        projectileRB.velocity = Vector2.right;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 vel;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        vel = new Vector2(Input.GetAxisRaw("Horizontal"), rb.velocity.y);
    }

    private void FixedUpdate()
    {
       rb.velocity = vel;
    }

}

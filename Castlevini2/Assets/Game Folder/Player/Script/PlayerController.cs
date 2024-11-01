using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 vel;

    public Transform skin;

    public Transform floorCollider;
    public LayerMask floorLayer;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Jump();
        Movement();
        
    }

    private void FixedUpdate()
    {
       rb.velocity = vel;
    }

    // MOVIMENTO ESQUERDA/DIREITA
    private void Movement()
    {
        vel = new Vector2(Input.GetAxisRaw("Horizontal"), rb.velocity.y);
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            skin.GetComponent<Animator>().SetBool("PlayerRun", true);
        }
        else
        {
            skin.GetComponent<Animator>().SetBool("PlayerRun", false);
        }
    }

    private void Jump()
    {
        bool canJump = Physics2D.OverlapCircle(floorCollider.position, 0.2f, floorLayer);
        if (Input.GetButtonDown("Jump") && canJump)
        {
            skin.GetComponent<Animator>().Play("PlayerJump",-1);
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, 250));
        }
        
    }
}

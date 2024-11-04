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

    public float walkSpeed;
    float jumpTime;
    int numCombo;
    float comboTime;
    float dashTime;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

        Dash();
        Death();
        Attack();
        Jump();
        Movement();
        skin.GetComponent<Animator>().SetFloat("yVelocity", vel.y);
    }

    private void FixedUpdate()
    {
        if(dashTime > 0.5)
        {
            rb.velocity = vel;
        }
    }

    // MOVIMENTO
    private void Movement()
    {
        vel = new Vector2(Input.GetAxisRaw("Horizontal")*walkSpeed, rb.velocity.y);
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            skin.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            skin.GetComponent<Animator>().SetBool("PlayerRun", true);
        }
        else
        {
            skin.GetComponent<Animator>().SetBool("PlayerRun", false);
        }
    }
    
    // DASH
    private void Dash()
    {
        dashTime = dashTime + Time.deltaTime;
        if (Input.GetButtonDown("Fire2") && dashTime > 1)
        {
            dashTime = 0;
            skin.GetComponent<Animator>().Play("PlayerDash", -1);
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(skin.localScale.x * 200, 0));
        }
    }

    // PULO
    private void Jump()
    {
        bool canJump = Physics2D.OverlapCircle(this.transform.position, 0.2f, floorLayer);
        jumpTime = jumpTime + Time.deltaTime;
        if (Input.GetButtonDown("Jump") && canJump && jumpTime > 1.5f)
        {
            jumpTime = 0;
            skin.GetComponent<Animator>().SetBool("Jump", true);
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, 300));
        }

        skin.GetComponent<Animator>().SetBool("Jump", !canJump);

    }

    // ATAQUES
    private void Attack()
    {
        comboTime = comboTime + Time.deltaTime;
        bool canJump = Physics2D.OverlapCircle(this.transform.position, 0.2f, floorLayer);
        if (canJump == false && Input.GetButtonDown("Fire1") && comboTime >0.5f)
        {
            numCombo++;
            if (numCombo > 1)
            {
                numCombo = 1;
            }
            comboTime = 0;
            //rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, 50));
            skin.GetComponent<Animator>().Play("PlayerJumpAttack2", -1);
        }

        if (Input.GetButtonDown("Fire1") && comboTime > 0.5f)
        {
            numCombo++;
            if (numCombo > 3) numCombo = 1;
            comboTime = 0;
            skin.GetComponent<Animator>().Play("PlayerAttack" + numCombo, -1);
        }
        if (comboTime >= 1)
        {
            numCombo = 0;
        }
    }

    // MORTE
    private void Death()
    {
        if (GetComponent<Character>().life <= 0)
        {
            this.enabled = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(floorCollider.position, 0.2f);
    }
}

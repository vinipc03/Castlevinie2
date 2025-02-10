using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellHoundController : MonoBehaviour
{
    public Transform skin;
    private Rigidbody2D rb;
    public CapsuleCollider2D houndcollider;
    public string layerName = "TilemapFront";

    private bool detectPlayer;
    [SerializeField] private float radius;
    [SerializeField] private float speed;
    public LayerMask playerLayer;
    public LayerMask floorLayer;
    private Transform player;
    
    public float posX;

    private bool canjump;
    private float timer;
    [SerializeField] private float jumpRadius;

    [Header("Jump Attacking")]
    [SerializeField] float jumpHeight;
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 boxSize;
    [SerializeField] private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        houndcollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRotation();
        Movement();
        GroundCheck();
        Death();
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        posX = player.transform.position.x - transform.position.x;
        isGrounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, floorLayer);
    }


    private void JumpAttack()
    {
        skin.GetComponent<Animator>().Play("Jump", -1);
        rb.AddForce(new Vector2(posX, jumpHeight), ForceMode2D.Impulse);
    }

    private void GroundCheck()
    {
        if (isGrounded)
        {
            houndcollider.isTrigger = false;
        }
        else
        {
            houndcollider.isTrigger = true;
        }
    }

    private void Movement()
    {
        detectPlayer = Physics2D.OverlapCircle(transform.position, radius, playerLayer);
        canjump = Physics2D.OverlapCircle(transform.position, jumpRadius, playerLayer);

        if (detectPlayer)
        {
            if (canjump)
            {
                skin.GetComponent<Animator>().SetBool("isRunning", false);
                if (timer > 1.5f)
                {
                    timer = 0;
                    JumpAttack();
                }
            }
            else
            {
                Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                skin.GetComponent<Animator>().SetBool("isRunning", true);
            }
        }
        else
        {
            skin.GetComponent<Animator>().SetBool("isRunning", false);
        }
    }

    void Death()
    {
        if (GetComponent<Character>().life <= 0)
        {
            //audioSource.PlayOneShot(die, 0.5f);
            GetComponent<CapsuleCollider2D>().enabled = false;
            this.enabled = false;
            rb.simulated = false;
        }
    }

    void UpdateRotation()
    {
        posX = player.transform.position.x - transform.position.x;
        // Define a rotação para apontar para o eixo X ou Y, dependendo da direção
        if (posX > 0)
        {
            // aponta para direita
            skin.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            // aponta para esquerda
            skin.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Character>().PlayerDamage(1);
            collision.GetComponent<PlayerController>().KnockBack(transform.position);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawWireSphere(transform.position, jumpRadius);

        Gizmos.DrawWireCube(groundCheck.position, boxSize);
    }
}

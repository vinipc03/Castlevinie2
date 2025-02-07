using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellHoundController : MonoBehaviour
{
    public Transform skin;
    private Rigidbody2D rb;
    private CapsuleCollider2D collider;
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
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        collider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, floorLayer);
        UpdateRotation();
        
        Movement();
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        posX = player.transform.position.x - transform.position.x;
        GroundCheck();
    }


    private void JumpAttack()
    {
        if (isGrounded)
        {
            isGrounded = false;
            rb.isKinematic = false;
            skin.GetComponent<Animator>().Play("Jump", -1);
            rb.AddForce(new Vector2(posX, jumpHeight), ForceMode2D.Impulse);
        }
    }

    private void GroundCheck()
    {
        if (isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true; // Mantenha a física ativa
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
                if (timer > 2)
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

    private void Attack()
    {
        skin.GetComponent<Animator>().Play("Jump", -1);

        rb.isKinematic = false; // Ativa a física antes do pulo

        // Se o inimigo está virado para a esquerda, aplica a força negativa, se estiver para a direita aplica a positiva.
        if (posX < 0)
        {
            rb.velocity = Vector2.zero; // Garante que não há movimento acumulado
            rb.AddForce(new Vector2(-220f, 100f));
        }
        else
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(220f, 100f));
        }


    }

    private IEnumerator Jump()
    {
        isGrounded = false;
        yield return new WaitForSeconds(0.5f);
        //canjump = false; // Impede que o inimigo tente pular várias vezes seguidas

        //skin.GetComponent<Animator>().Play("Jump", -1);
        //rb.isKinematic = false; // Ativa a física antes do pulo
        //rb.velocity = Vector2.zero; // Garante que não há movimento acumulado

        //if (posX < 0)
        //{
        //    rb.AddForce(new Vector2(-15f, 30f), ForceMode2D.Impulse);
        //}
        //else
        //{
        //    rb.AddForce(new Vector2(15f, 30f), ForceMode2D.Impulse);
        //}

        //yield return new WaitForSeconds(2f);

        //rb.velocity = Vector2.zero; // Para o movimento após o pulo
        //canjump = true; // Permite outro pulo depois de um tempo
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
        //if (isGrounded)
        //{
        //    if (collision.CompareTag("Floor") || collision.gameObject.layer == LayerMask.NameToLayer(layerName))
        //    {
        //        rb.velocity = Vector2.zero;
        //        rb.isKinematic = true; // Mantenha a física ativa
        //    }
        //}

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawWireSphere(transform.position, jumpRadius);

        Gizmos.DrawCube(groundCheck.position, boxSize);
    }
}

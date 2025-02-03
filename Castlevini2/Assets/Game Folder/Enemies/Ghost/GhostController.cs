using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public Transform skin;
    
    private bool detectPlayer;
    public LayerMask playerLayer;
    private Transform player;
    public float posX;

    [SerializeField] private float radius;
    [SerializeField] private float speed;
    [SerializeField] private bool attack;
    [SerializeField] private float attackRange;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        posX = player.transform.position.x - transform.position.x;
        Movement();
        UpdateRotation();
        Death();
    }

    private void Movement()
    {
        detectPlayer = Physics2D.OverlapCircle(transform.position, radius, playerLayer);
        attack = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);

        if (detectPlayer && Vector2.Distance(transform.position, player.GetComponent<CapsuleCollider2D>().bounds.center) > 0.5f)
        {
            skin.GetComponent<Animator>().SetBool("detectPlayer", true);
            transform.position = Vector2.MoveTowards(transform.position, player.GetComponent<CapsuleCollider2D>().bounds.center, speed * Time.deltaTime);
            if(timer > 2 && attack == true)
            {
                timer = 0;
                skin.GetComponent<Animator>().Play("Attack", -1);
            }
        }
        else
        {
            skin.GetComponent<Animator>().SetBool("detectPlayer", false);
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

    void Death()
    {
        if (GetComponent<Character>().life <= 0)
        {
            GetComponent<CircleCollider2D>().enabled = false;
            this.enabled = false;
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
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonClothedController : MonoBehaviour
{
    [SerializeField] private float radius;
    private bool detectPlayer;
    public LayerMask playerLayer;
    public Transform skin;

    public Transform obstacleCollider;
    [SerializeField] private float obstacleRadius;
    private bool detectObstacle;
    public LayerMask floorLayer;
    [SerializeField] private bool goRight = false;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
        Movement();
        Death();
    }

    private void DetectPlayer()
    {
        detectPlayer = Physics2D.OverlapCircle(transform.position, radius, playerLayer);
        if (detectPlayer)
        {
            GetComponent<CapsuleCollider2D>().enabled = true;
            skin.GetComponent<Animator>().SetBool("detectPlayer", true);
        }
    }

    private void Movement()
    {
        detectObstacle = Physics2D.OverlapCircle(obstacleCollider.position, obstacleRadius, floorLayer);
        if (detectObstacle)
        {
            if (goRight) goRight = false;
            else goRight = true;
        }

        Vector3 targetPosition = new Vector3(obstacleCollider.position.x, transform.position.y, transform.position.z);

        if (goRight)
        {
            skin.localScale = new Vector3(-1, 1, 1);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            skin.localScale = new Vector3(1, 1, 1);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }


    }

    void Death()
    {
        if (GetComponent<Character>().life <= 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
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
        Gizmos.DrawWireSphere(obstacleCollider.position, obstacleRadius);
    }
}

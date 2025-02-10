using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UbdeadHandController : MonoBehaviour
{
    [SerializeField]private float radius;
    private bool detectPlayer;
    public LayerMask playerLayer;
    public Transform skin;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
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

    void Death()
    {
        if (GetComponent<Character>().life <= 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            this.enabled = false;
        }
    }

    private void ReleasePlayer()
    {
        player.GetComponent<PlayerController>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;
            collision.GetComponent<Character>().PlayerDamage(1);
            collision.GetComponent<PlayerController>().enabled = false;
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            
            Invoke("ReleasePlayer", 1.5f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

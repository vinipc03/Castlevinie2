using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UbdeadHandController : MonoBehaviour
{
    [SerializeField]private float radius;
    private bool detectPlayer;
    public LayerMask playerLayer;
    public Transform skin;

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
    }
}

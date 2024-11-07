using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    public Transform a;
    public Transform b;

    public Transform skin;

    public bool goLeft;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Death();
    }

    // Lógica de patrulha entre os pontos A e B
    public void Movement()
    {
        if (goLeft == true)
        {
            skin.localScale = new Vector3(1, 1, 1);
            if (Vector2.Distance(transform.position, a.position) < 0.1f)
            {
                goLeft = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, a.position, 1 * Time.deltaTime);
        }
        else
        {
            skin.localScale = new Vector3(-1, 1, 1);
            if (Vector2.Distance(transform.position, b.position) < 0.1f)
            {
                goLeft = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, b.position, 1 * Time.deltaTime);
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
}

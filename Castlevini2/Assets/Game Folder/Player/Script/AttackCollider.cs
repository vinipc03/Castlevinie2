using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public Transform player;
    public GameObject impactEffect;
    private float horizontal;
    [HideInInspector] private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (player.GetComponent<PlayerController>().numCombo == 1)
            {
                collision.GetComponent<Character>().life--;
                collision.GetComponentInChildren<Animator>().Play("TakeHit", -1);
                Destroy(impactEffect, 1f);
                Instantiate(impactEffect, transform.position, transform.rotation);
            }
            if (player.GetComponent<PlayerController>().numCombo == 2)
            {
                collision.GetComponent<Character>().life -= 1;
                collision.GetComponentInChildren<Animator>().Play("TakeHit", -1);
                Destroy(impactEffect, 1f);
                Instantiate(impactEffect, transform.position, transform.rotation);
            }
            if (player.GetComponent<PlayerController>().numCombo == 3)
            {
                collision.GetComponent<Character>().life -= 2;
                collision.GetComponentInChildren<Animator>().Play("TakeHit", -1);
                Destroy(impactEffect, 1f);
                Instantiate(impactEffect, transform.position, transform.rotation);
            }
            if (player.GetComponent<PlayerController>().handlingObj == 1 && player.GetComponent<PlayerController>().holySlash == true)
            {
                collision.GetComponent<Character>().life -= 5;
                collision.GetComponentInChildren<Animator>().Play("TakeHit", -1);
                player.GetComponent<PlayerController>().holySlash = false;
                Destroy(impactEffect, 1f);
                Instantiate(impactEffect, transform.position, transform.rotation);
            }
        }
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}

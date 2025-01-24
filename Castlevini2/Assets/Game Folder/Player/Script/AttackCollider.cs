using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public Transform player;
    public GameObject impactEffect;
    private float horizontal;
    private bool isFacingRight = true;
    public AudioSource audioSource;
    public AudioClip hitSound;

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
                audioSource.PlayOneShot(hitSound, 0.3f);
                collision.GetComponent<Character>().life--;
                collision.GetComponentInChildren<Animator>().Play("TakeHit", -1);
                Instantiate(impactEffect, transform.position, transform.rotation);
            }
            if (player.GetComponent<PlayerController>().numCombo == 2)
            {
                audioSource.PlayOneShot(hitSound, 0.3f);
                collision.GetComponent<Character>().life -= 1;
                collision.GetComponentInChildren<Animator>().Play("TakeHit", -1);
                Instantiate(impactEffect, transform.position, transform.rotation);
            }
            if (player.GetComponent<PlayerController>().numCombo == 3)
            {
                audioSource.PlayOneShot(hitSound, 0.3f);
                collision.GetComponent<Character>().life -= 2;
                collision.GetComponentInChildren<Animator>().Play("TakeHit", -1);
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

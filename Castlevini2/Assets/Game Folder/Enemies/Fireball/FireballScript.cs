using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public GameObject fireEffect;

    public AudioSource audioSource;
    public AudioClip explosion;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(this.gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            collision.GetComponent<Character>().PlayerDamage(1);
            collision.GetComponent<PlayerController>().KnockBack(transform.position);
            Instantiate(fireEffect, transform.position, transform.rotation);
            audioSource.PlayOneShot(explosion, 0.5f);
        }
    }
}

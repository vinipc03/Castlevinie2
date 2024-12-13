using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolySlash : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(this.gameObject, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(impactEffect, transform.position, transform.rotation);

        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Character>().PlayerDamage(3);
        }

    }
}

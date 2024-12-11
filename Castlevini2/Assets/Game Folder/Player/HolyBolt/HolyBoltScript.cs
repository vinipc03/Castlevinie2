using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyBoltScript : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(this.gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Destroy(impactEffect, 1f);
        Instantiate(impactEffect, transform.position, transform.rotation);
        
        if (collision.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
            collision.GetComponent<Character>().PlayerDamage(2);
        }
        
    }
}

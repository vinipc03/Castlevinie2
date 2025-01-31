using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStrike : MonoBehaviour
{
    public GameObject impactEffect;
    void Start()
    {
        Destroy(this.gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            collision.GetComponent<Character>().PlayerDamage(2);
            collision.GetComponent<Animator>().Play("TakeHit", -1);
        }
    }
}

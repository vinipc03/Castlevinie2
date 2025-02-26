﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDamageController : MonoBehaviour
{

    [SerializeField] private float force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force), ForceMode2D.Impulse);
            collision.GetComponent<Character>().PlayerDamage(1);
        }
    }
}

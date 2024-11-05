using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public Transform player;

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
        if (collision.CompareTag("Enemy"))
        {
            if (player.GetComponent<PlayerController>().numCombo == 1)
            {
                collision.GetComponent<Character>().life--;
                collision.GetComponentInChildren<Animator>().Play("TakeHit", -1);
            }
            if (player.GetComponent<PlayerController>().numCombo == 2)
            {
                collision.GetComponent<Character>().life -= 1;
                collision.GetComponentInChildren<Animator>().Play("TakeHit", -1);
            }
            if (player.GetComponent<PlayerController>().numCombo == 3)
            {
                collision.GetComponent<Character>().life -= 2;
                collision.GetComponentInChildren<Animator>().Play("TakeHit", -1);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUpScript : MonoBehaviour
{
    public int hpPot;
    public int mpPot;
    public int hpHeal;
    public int mpHeal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // POÇÃO DE VIDA
            collision.GetComponent<PlayerController>().hpPotCount += hpPot;

            // POÇÃO DE MANA
            collision.GetComponent<PlayerController>().mpPotCount += mpPot;

            // FRANGO
            if(hpHeal > 0)
            {
                collision.GetComponent<Character>().HpHeal(hpHeal);
                collision.GetComponentInChildren<Animator>().Play("PlayerHpHeal", 1);
            }

            // MAÇA
            if(mpHeal > 0)
            {
                collision.GetComponent<Character>().MpHeal(mpHeal);
                collision.GetComponentInChildren<Animator>().Play("PlayerMpHeal", 1);
            }

            Destroy(this.gameObject);
        }
    }

}

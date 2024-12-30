using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUpScript : MonoBehaviour
{
    private Rigidbody2D itemRb;
    public string layerName = "TilemapFront";
    public float dropForce = 2;
    public int hpPot;
    public int mpPot;
    public int hpHeal;
    public int mpHeal;

    private void Start()
    {
        itemRb = GetComponent<Rigidbody2D>();
        itemRb.AddForce(Vector2.up * dropForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        PlayerController playerController = collision.GetComponent<PlayerController>();

        // PARA NO TILE FLOOR
        if (collision.CompareTag("Floor"))
        {
            // Para o movimento após cair no chão
            itemRb.velocity = Vector2.zero;
            itemRb.isKinematic = true; // Desativa física
        }

        // PARA EM ONE WAY PLATFORMS
        if (collision.gameObject.layer == LayerMask.NameToLayer(layerName))
        {
            // Para o movimento após cair no chão
            itemRb.velocity = Vector2.zero;
            itemRb.isKinematic = true; // Desativa física
        }

        if (collision.CompareTag("Player"))
        {

            if (playerController != null)
            {
                // POÇÃO DE VIDA
                if (hpPot >= 1)
                {
                    playerController.hpPotCount += hpPot;
                }

                // POÇÃO DE MANA
                if (mpPot >= 1)
                {
                    playerController.mpPotCount += mpPot;
                }

                // TOCA O SOM
                if (playerController.audioSource != null && playerController.powerUpSound != null)
                {
                    playerController.audioSource.PlayOneShot(playerController.powerUpSound, 0.3f);
                }


                // FRANGO
                if (hpHeal > 0)
                {
                    collision.GetComponent<Character>().HpHeal(hpHeal);
                    collision.GetComponentInChildren<Animator>().Play("PlayerHpHeal", 1);
                }

                // MAÇA
                if (mpHeal > 0)
                {
                    collision.GetComponent<Character>().MpHeal(mpHeal);
                    collision.GetComponentInChildren<Animator>().Play("PlayerMpHeal", 1);
                }

                Destroy(this.gameObject);
            }
        }
    }
}

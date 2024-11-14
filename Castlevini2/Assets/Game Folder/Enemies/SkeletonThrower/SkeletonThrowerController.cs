using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonThrowerController : MonoBehaviour
{
    public float visionRadius = 8f; // Raio de visão do inimigo
    private float timer;

    public Transform skin;
    private Transform player;
    // Calcula a direção para o jogador
    public float posX;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        AggroVision();
        UpdateRotation();
        Death();
        
    }

    

    void AggroVision()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < visionRadius)
        {
            if(distance < 2.5f && timer >2)
            {
                timer = 0;
                skin.GetComponent<Animator>().Play("AttackMelee", -1);
            }
            else
            {
                if (timer > 2)
                {
                    timer = 0;
                    skin.GetComponent<Animator>().Play("AttackThrow", -1);
                }
            }
            
        }
    }

    

    void UpdateRotation()
    {
        posX = player.transform.position.x - transform.position.x;
        // Define a rotação para apontar para o eixo X ou Y, dependendo da direção
        if (posX > 0)
        {
            // aponta para direita
            skin.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            // aponta para esquerda
            skin.localScale = new Vector3(-1, 1, 1);
        }
    }

    void Death()
    {
        if (GetComponent<Character>().life <= 0)
        {
            //AggroVision().enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
            this.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Character>().PlayerDamage(1);
            collision.GetComponent<PlayerController>().KnockBack(transform.position);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, visionRadius);

    }
}

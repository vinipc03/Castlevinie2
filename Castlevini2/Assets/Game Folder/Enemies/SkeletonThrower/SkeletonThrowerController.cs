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
    public bool isAttacking = false;

    public AudioSource audioSource;
    public AudioClip dieSound;
    public AudioClip attackMelee;
    public AudioClip attackThrow;

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

            Debug.Log("Player entrou na área de visão");
            if (distance < 2.5f && timer >2f)
            {
                isAttacking = true;
                audioSource.PlayOneShot(attackMelee, 0.5f);
                timer = 0;
                skin.GetComponent<Animator>().Play("AttackMelee", -1);
                
            }
            else
            {
                if (timer > 2)
                {
                    audioSource.PlayOneShot(attackThrow, 0.8f);
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
            audioSource.PlayOneShot(dieSound, 0.5f);
            GetComponent<CapsuleCollider2D>().enabled = false;
            this.enabled = false;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player") && !isAttacking)
    //    {
    //        Debug.Log("Causou dano no player encostando no skeleton");
    //        collision.GetComponent<Character>().PlayerDamage(1);
    //        collision.GetComponent<PlayerController>().KnockBack(transform.position);
    //    }
    //}


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, visionRadius);

    }
}

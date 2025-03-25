using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheDemonController : MonoBehaviour
{
    public Transform skin;
    public float walkSpeed;
    private Transform player;

    // Calcula a direção para o jogador
    public float posX;
    [SerializeField] LayerMask playerLayer;

    // range de visão
    public bool aggroVision = false;
    [SerializeField] float aggroVisionRadius;
   
    //Range de ataque melee
    public bool meleeRange;
    [SerializeField] float meleeRangeRadius;
    


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        Attack();
        if (skin.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            return;
        }
        Movement();
        UpdateRotation();
    }

    private void FixedUpdate()
    {
        
        
    }

    private void Movement()
    {
        aggroVision = Physics2D.OverlapCircle(transform.position, aggroVisionRadius, playerLayer);

        if (aggroVision)
        {
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, walkSpeed * Time.deltaTime);
            skin.GetComponent<Animator>().SetBool("isRunning", true);
        }
    }

    private void Attack()
    {
        meleeRange = Physics2D.OverlapCircle(transform.position, meleeRangeRadius, playerLayer);

        if (meleeRange)
        {
            skin.GetComponent<Animator>().Play("Attack2", -1);
            return;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, aggroVisionRadius);
        Gizmos.DrawWireSphere(transform.position, meleeRangeRadius);
    }
}

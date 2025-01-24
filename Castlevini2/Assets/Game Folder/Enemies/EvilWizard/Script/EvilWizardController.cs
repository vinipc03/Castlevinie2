using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizardController : MonoBehaviour
{
    public Transform skin;
    public float walkSpeed;
    private Transform player;

    // Calcula a direção para o jogador
    public float posX;
    private float timer;

    public bool meleeRange;
    public float meleeRangeRadius;
    [SerializeField] LayerMask playerLayer;

    [SerializeField] private bool distanceRange;
    public float distanceRangeRadius;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Death();
        timer += Time.deltaTime;
        posX = player.transform.position.x - transform.position.x;
        if (skin.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("AttackMelee"))
        {
            return;
        }
        MeleeAggroVision();
        RangedAggroVision();
        UpdateRotation();
    }

    private void MeleeAggroVision()
    {
        meleeRange = Physics2D.OverlapCircle(transform.position, meleeRangeRadius, playerLayer);
        float distance = Vector3.Distance(transform.position, player.position);

        // Logica de perseguição
        if (meleeRange)
        {
            if (distance> 2f)
            {
                Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, walkSpeed * Time.deltaTime);
                skin.GetComponent<Animator>().SetBool("isRunning", true);
            }
            else
            {
                skin.GetComponent<Animator>().SetBool("isRunning", false);
            }

            // ataca o player
            if (distance < 5f && timer > 3f)
            {
                timer = 0;
                skin.GetComponent<Animator>().Play("AttackMelee", -1);
            }
        }
        else
        {
            skin.GetComponent<Animator>().SetBool("isRunning", false);
        }
    }

    private void RangedAggroVision()
    {
        distanceRange = Physics2D.OverlapCircle(transform.position, distanceRangeRadius, playerLayer);

        if (!meleeRange)
        {
            if (distanceRange)
            {
                if (timer > 2f)
                {
                    timer = 0;
                    skin.GetComponent<Animator>().Play("AttackRanged");
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
            GetComponent<CapsuleCollider2D>().enabled = false;
            this.enabled = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, meleeRangeRadius);
        Gizmos.DrawWireSphere(transform.position, distanceRangeRadius);
        //Gizmos.DrawWireSphere(overHeadCollider.position, overHeadRadius);
    }
}

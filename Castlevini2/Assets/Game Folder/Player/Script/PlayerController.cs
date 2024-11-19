using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 vel;
    private Vector2 position;
    private Vector2 colliderSize;
    private CapsuleCollider2D playerCollider;
    public Transform skin;
    public Transform floorCollider;
    public LayerMask floorLayer;
    [HideInInspector] public int handlingObj;

    [Header("Potions")]
    public int hpPotCount;
    public int mpPotCount;
    [HideInInspector] public int maxHpPotCount;
    [HideInInspector] public int maxMpPotCount;

    [Header("Movements")]
    public float walkSpeed;
    [HideInInspector] public bool canJump;
    public float jumpForce;
    float jumpTime;
    float dashTime;
    public float kbForce;
    private bool isKnockedBack = false;
    private float knockbackDuration = 0.5f;


    [Header("Slopes")]
    [SerializeField] private float slopeCheckDistance;
    private float slopeAngle;
    private bool isOnSlope;
    [SerializeField] private PhysicsMaterial2D noFrictionMaterial;
    [SerializeField] private PhysicsMaterial2D frictionMaterial;
    private Vector2 perpendicularSpeed;


    [Header("Combat")]
    [HideInInspector] public int numCombo;
    [HideInInspector] public bool holySlash;
    float comboTime;
    float spellTime;
    [HideInInspector] public bool onAttack;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maxHpPotCount = 3;
        maxMpPotCount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position - new Vector3(0f, colliderSize.y / 2, 0f);
        DetectSlopes();
        if (!onAttack && !isKnockedBack)
        {
            Movement();
        }
        Jump();
        Habilities();
        Dash();
        Attack();
        Death();
        skin.GetComponent<Animator>().SetFloat("yVelocity", rb.velocity.y);
    }

    private void FixedUpdate()
    {
        
        if (dashTime > 0.5)
        {
            rb.velocity = vel;
        }

    }

    // EFEITO REPULSÃO
    public void KnockBack(Vector3 enemyPosition)
    {
        // Determina a direção do knockback com base na posição do inimigo
        float direction = transform.position.x < enemyPosition.x ? -1 : 1;

        // Aplica a força do knockback na direção oposta ao inimigo
        rb.velocity = new Vector2(direction * kbForce, rb.velocity.y);

        // Ativa o estado de knockback
        isKnockedBack = true;
        Invoke("EndKnockback", knockbackDuration);
    }

    private void EndKnockback()
    {
        isKnockedBack = false;
    }

    // MOVIMENTO
    private void Movement()
    {
        vel = new Vector2(Input.GetAxisRaw("Horizontal") * walkSpeed, rb.velocity.y);
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            skin.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            skin.GetComponent<Animator>().SetBool("PlayerRun", true);
        }
        else
        {
            skin.GetComponent<Animator>().SetBool("PlayerRun", false);
        }
    }

    private void DetectSlopes()
    {
        RaycastHit2D hitSlope = Physics2D.Raycast(this.position, Vector2.down, slopeCheckDistance, floorLayer);
        if (hitSlope)
        {
            //perpendicularSpeed = Vector2.Perpendicular(hitSlope.normal).normalized;
            slopeAngle = Vector2.Angle(hitSlope.normal, Vector2.up);
            isOnSlope = slopeAngle != 0;
        }

        if (isOnSlope && Input.GetAxisRaw("Horizontal") == 0)
        {
            rb.sharedMaterial = frictionMaterial;
        }
        else
        {
            rb.sharedMaterial = noFrictionMaterial;
        }
    }
    
    // DASH
    private void Dash()
    {
        dashTime = dashTime + Time.deltaTime;
        if (Input.GetButtonDown("Fire2") && dashTime > 1)
        {
            dashTime = 0;
            skin.GetComponent<Animator>().Play("PlayerDash", -1);
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(skin.localScale.x * 200, 0));
            rb.gravityScale = 0;
            Invoke("RestoreGravityScale", 0.25f);
        }
    }

    void RestoreGravityScale()
    {
        rb.gravityScale = 2;
    } //RESTAURA GRAVIDADE

    // PULO
    private void Jump()
    {
        canJump = Physics2D.OverlapCircle(this.transform.position, 0.2f, floorLayer);
        jumpTime = jumpTime + Time.deltaTime;
        if (Input.GetButtonDown("Jump") && canJump && jumpTime > 1f)
        {
            jumpTime = 0;
            skin.GetComponent<Animator>().SetBool("Jump", true);
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, jumpForce));
        }
        skin.GetComponent<Animator>().SetBool("Jump", !canJump);
    }

    // ATAQUES
    private void Attack()
    {
        comboTime = comboTime + Time.deltaTime;
        bool canJump = Physics2D.OverlapCircle(this.transform.position, 0.2f, floorLayer);
        if (canJump == false && Input.GetButtonDown("Fire1") && comboTime >0.5f)
        {
            numCombo++;
            if (numCombo > 1)
            {
                numCombo = 1;
            }
            comboTime = 0;
            //rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, 50));
            skin.GetComponent<Animator>().Play("PlayerJumpAttack2", -1);
        }

        if (Input.GetButtonDown("Fire1") && comboTime > 0.5f)
        {
            numCombo++;
            if (numCombo > 3) numCombo = 1;
            comboTime = 0;
            skin.GetComponent<Animator>().Play("PlayerAttack" + numCombo, -1);
            onAttack = true;

        }
        if (comboTime >= 1)
        {
            numCombo = 0;
        }
    }

    public void Habilities()
    {
        // TECLA 1 POÇÃO DE VIDA
        if (hpPotCount >= 1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && canJump)
            {
                onAttack = true;
                skin.GetComponent<Animator>().Play("DrinkHp", -1);
                skin.GetComponent<Animator>().Play("PlayerHpHeal", 1);
                this.GetComponent<Character>().HpHeal(5);
                hpPotCount--;
            }
        }

        // TECLA 2 POÇÃO DE MANA
        if (mpPotCount >= 1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2) && canJump)
            {
                onAttack = true;
                skin.GetComponent<Animator>().Play("DrinkMp", -1);
                skin.GetComponent<Animator>().Play("PlayerMpHeal", 1);
                this.GetComponent<Character>().MpHeal(5);
                mpPotCount--;
            }
        }

        // TECLAS DE ATALHO PARA PODERES
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            handlingObj = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            handlingObj = 1;
        }

        spellTime = spellTime + Time.deltaTime;
        // BOTÃO 3 HOLY BOLT
        if (handlingObj == 0)
        {
            if (this.GetComponent<Character>().mana > 0)
            {
                if (Input.GetButtonDown("Fire3"))
                {
                    if (spellTime >= 1)
                    {
                        onAttack = true;
                        skin.GetComponent<Animator>().Play("HolyBolt", -1);
                        this.GetComponent<Character>().MpDecrease(1);
                        spellTime = 0;
                    }
                }
            }

        }

        // BOTÃO 4 HOLY SLASH
        if (handlingObj == 1)
        {
            if (this.GetComponent<Character>().mana > 1 && canJump)
            {
                if (Input.GetButtonDown("Fire3"))
                {
                    if (spellTime >= 1)
                    {
                        onAttack = true;
                        holySlash = true;
                        skin.GetComponent<Animator>().Play("PlayerHolySlash", -1);
                        this.GetComponent<Character>().MpDecrease(2);
                        spellTime = 0;
                    }
                }
            }
        }
    }

    // MORTE
    private void Death()
    {
        if (GetComponent<Character>().life <= 0)
        {
            this.enabled = false;
            rb.simulated = false;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(floorCollider.position, 0.2f);
    }
}

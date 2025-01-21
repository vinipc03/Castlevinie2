using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 vel;
    private Vector2 position;
    private Vector2 colliderSize;
    public Transform skin;
    public Transform floorCollider;
    public LayerMask floorLayer;
    [HideInInspector] public int handlingObj;
    private int powerSelected = 0;

    public Transform gameOverScreen;
    public Transform pauseScreen;
    [HideInInspector] public bool isPaused = false;
    public string currentLevel;

    [Header("Potions")]
    public int hpPotCount;
    public int mpPotCount;
    [HideInInspector] public int maxHpPotCount;
    [HideInInspector] public int maxMpPotCount;

    [Header("Movements")]
    public float walkSpeed;
    [HideInInspector] public bool canJump;
    public bool isJumping = false;
    public float jumpForce;
    float jumpTime;
    float dashTime;
    public float kbForce;
    private bool isKnockedBack = false;
    private float knockbackDuration = 0.5f;
    private float moveInput;
    private bool isCrouch = false;


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
    float potTime = 0;
    [HideInInspector] public bool onAttack = false;

    [Header("Sounds")]
    public AudioSource audioSource;
    public AudioClip attack1Sound;
    public AudioClip attack2Sound;
    public AudioClip attack3Sound;
    public AudioClip takeDamageSound;
    public AudioClip dashSound;
    public AudioClip holyBoltSound;
    public AudioClip holySlashSound;
    public AudioClip lightStrikeSound;
    public AudioClip powerUpSound;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        maxHpPotCount = 3;
        maxMpPotCount = 3;
        currentLevel = SceneManager.GetActiveScene().name;
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentLevel.Equals(SceneManager.GetActiveScene().name))
        {
            currentLevel = SceneManager.GetActiveScene().name;
            transform.position = GameObject.Find("Spawn").transform.position;
        }

        if (isPaused) return;

        Pause();
        DetectSlopes();
        Crouch();
        Movement();
        Jump();
        Habilities();
        Dash();
        Attack();
        Death();
        PotionControl();
        skin.GetComponent<Animator>().SetFloat("yVelocity", rb.velocity.y);
        position = transform.position - new Vector3(0f, colliderSize.y / 2, 0f);
        
    }

    private void FixedUpdate()
    {
        if (onAttack)
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // Zera apenas o movimento horizontal
        }
        else if (!isKnockedBack)
        {
            if (dashTime > 0.5)
            {
                rb.velocity = vel;
            }
        }

        moveInput = Input.GetAxisRaw("Horizontal");
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

    private void Crouch()
    {
        if(Input.GetKeyDown(KeyCode.S) && canJump)
        {
            skin.GetComponent<Animator>().SetBool("isCrouch", true);
            isCrouch = true;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            skin.GetComponent<Animator>().SetBool("isCrouch", false);
            isCrouch = false;
        }
    }

    // MOVIMENTO
    private void Movement()
    {
        vel = new Vector2(moveInput * walkSpeed, rb.velocity.y);

        if (!onAttack) // Apenas permite movimento se não estiver atacando
        {
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
        else
        {
            vel = new Vector2(0, rb.velocity.y); // Garante que vel.x é 0
            skin.GetComponent<Animator>().SetBool("PlayerRun", false);
        }
    }

    private void DetectSlopes()
    {
        RaycastHit2D hitSlope = Physics2D.Raycast(this.position, Vector2.down, slopeCheckDistance, floorLayer);
        if (hitSlope)
        {
            perpendicularSpeed = Vector2.Perpendicular(hitSlope.normal).normalized;
            slopeAngle = Vector2.Angle(hitSlope.normal, Vector2.up);
            isOnSlope = slopeAngle != 0;
        }

        if (isOnSlope && Input.GetAxisRaw("Horizontal") != 0)
        {
            Vector2 newVelocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);
            vel = Vector2.Lerp(rb.velocity, newVelocity, Time.deltaTime * 10f);
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
            audioSource.PlayOneShot(dashSound, 0.4f);
            onAttack = false;
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
        if (Input.GetButtonDown("Jump") && canJump && dashTime > 0.25f)
        {
            skin.GetComponent<Animator>().SetBool("Jump", true);
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, jumpForce));
            //rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        skin.GetComponent<Animator>().SetBool("Jump", !canJump);
    }

    // ATAQUES
    private void Attack()
    {
        // ATAQUE NO AR
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
            rb.AddForce(new Vector2(0, 100));
            skin.GetComponent<Animator>().Play("PlayerJumpAttack2", -1);
            audioSource.PlayOneShot(attack1Sound, 0.4f);
        }

        // ATAQUE CROUCHING
        if(Input.GetButtonDown("Fire1") && comboTime > 0.7f && isCrouch == true)
        {
            comboTime = 0;
            numCombo++;
            if (numCombo > 1)
            {
                numCombo = 1;
            }
            skin.GetComponent<Animator>().Play("PlayerCrouchAttack", -1);
            audioSource.PlayOneShot(attack1Sound, 0.4f);
        }

        // ATAQUE NO CHÃO
        if (Input.GetButtonDown("Fire1") && comboTime > 0.3f && !isCrouch)
        {
            onAttack = true;
            numCombo++;
            if (numCombo > 3) numCombo = 1;
            comboTime = 0;
            skin.GetComponent<Animator>().Play("PlayerAttack" + numCombo, -1);

            if (numCombo == 1)
            {
                audioSource.PlayOneShot(attack1Sound, 0.4f);
            }

            if (numCombo == 2)
            {
                audioSource.PlayOneShot(attack2Sound, 0.4f);
            }

            if (numCombo == 3)
            {
                audioSource.PlayOneShot(attack3Sound, 0.4f);
            }
        }
        if (comboTime >= 1)
        {
            numCombo = 0;
        }

    }

    public void Habilities()
    {
        potTime = potTime + Time.deltaTime;
        // TECLA 1 POÇÃO DE VIDA
        if (hpPotCount >= 1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && canJump && potTime > 1)
            {
                onAttack = true;
                skin.GetComponent<Animator>().Play("DrinkHp", -1);
                skin.GetComponent<Animator>().Play("PlayerHpHeal", 1);
                this.GetComponent<Character>().HpHeal(3);
                hpPotCount--;
                potTime = 0;
            }
        }

        // TECLA 2 POÇÃO DE MANA
        if (mpPotCount >= 1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2) && canJump && potTime > 1)
            {
                onAttack = true;
                skin.GetComponent<Animator>().Play("DrinkMp", -1);
                skin.GetComponent<Animator>().Play("PlayerMpHeal", 1);
                this.GetComponent<Character>().MpHeal(3);
                mpPotCount--;
                potTime = 0;
            }
        }

        // TECLAS DE ATALHO PARA PODERES
        handlingObj = powerSelected;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            powerSelected++;
            if(powerSelected > 2)
            {
                powerSelected = 0;
            }
        }

        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    handlingObj = 0;
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    handlingObj = 1;
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha5))
        //{
        //    handlingObj = 2;
        //}

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

        // BOTÃO HOLY EXPLOSION
        if (handlingObj == 2)
        {
            if (this.GetComponent<Character>().mana > 0 && canJump)
            {
                if (Input.GetButtonDown("Fire3"))
                {
                    if (spellTime >= 1)
                    {
                        onAttack = true;
                        skin.GetComponent<Animator>().Play("PlayerHolyExplosion", -1);
                        this.GetComponent<Character>().MpDecrease(1);
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
            gameOverScreen.GetComponent<GameOver>().enabled = true;
            this.enabled = false;
            rb.simulated = false;
        }
    }

    private void PotionControl()
    {
        if(hpPotCount > maxHpPotCount)
        {
            hpPotCount = maxHpPotCount;
        }

        if(mpPotCount > maxMpPotCount)
        {
            mpPotCount = maxMpPotCount;
        }
    }

    private void Pause()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            pauseScreen.GetComponent<Pause>().enabled = !pauseScreen.GetComponent<Pause>().enabled;
        }
    }

    public void ReturnToGame()
    {
        pauseScreen.GetComponent<Pause>().enabled = !pauseScreen.GetComponent<Pause>().enabled;
    }

    public void DestroyPlayer()
    {
        Destroy(transform.gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(floorCollider.position, 0.2f);
    }
}

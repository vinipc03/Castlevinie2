using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int life;
    private int maxLife;
    public int mana;
    private int maxMana;
    public Transform skin;

    public LifeController lifeBar;
    public ManaController manaBar;
    public float kbForce;
    private PlayerController playerController;

    [Header("iframes")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        //spriteRend = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        maxLife = life;
        maxMana = mana;
        playerController = GetComponent<PlayerController>();
        if (transform.CompareTag("Player"))
        {
            lifeBar.setMaxLife(life);
            manaBar.setMaxMana(mana);
        }
    }

    // Update is called once per frame
    void Update()
    {
        LifeControl();
        ManaControl();
        Death();
    }

    public void Death()
    {
        if (life <= 0)
        {
            skin.GetComponent<Animator>().Play("Die", -1);
        }
    }

    public void PlayerDamage(int value)
    {
        life = life - value;
        skin.GetComponent<Animator>().Play("TakeHit", -1);
        if (transform.CompareTag("Player"))
        {
            GetComponent<PlayerController>().audioSource.PlayOneShot(GetComponent<PlayerController>().takeDamageSound, 0.4f);
            lifeBar.SetLife(life);
            GetComponent<PlayerController>().onAttack = false;
            StartCoroutine(Invunerability());
        }
    }

    public void MpDecrease(int value)
    {
        mana = mana - value;
        manaBar.SetMana(mana);
    }

    public void HpHeal(int value)
    {
        GetComponent<PlayerController>().audioSource.PlayOneShot(GetComponent<PlayerController>().powerUpSound, 0.2f);
        life = life + value;
        lifeBar.SetLife(life);
    }

    public void MpHeal(int value)
    {
        GetComponent<PlayerController>().audioSource.PlayOneShot(GetComponent<PlayerController>().powerUpSound, 0.2f);
        mana = mana + value;
        manaBar.SetMana(mana);
    }

    public void LifeControl()
    {
        if (life < 1)
        {
            life = 0;
        }

        if (life > maxLife)
        {
            life = maxLife;
        }
    }

    public void ManaControl()
    {
        if (mana < 1)
        {
            mana = 0;
        }

        if (mana > maxMana)
        {
            mana = maxMana;
        }
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            skin.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes*2));
            skin.GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }
}

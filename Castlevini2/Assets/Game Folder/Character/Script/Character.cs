using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int life;
    public int maxLife;
    public int mana;
    public int maxMana;
    public Transform skin;

    public LifeController lifeBar;
    public ManaController manaBar;
    public float kbForce;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        maxLife = life;
        lifeBar.setMaxLife(life);
        maxMana = mana;
        manaBar.setMaxMana(mana);
        playerController = GetComponent<PlayerController>();
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
        //if (playerController != null && playerController.isBloking == true)
        //{
        //    skin.GetComponent<Animator>().Play("PlayerBlocked", -1);
        //    skin.GetComponent<Animator>().SetBool("Defend", false);
        //    playerController.GetComponent<PlayerController>().isBloking = false;
        //    return;
        //} ctrl+k+u para voltar
        life = life - value;
        skin.GetComponent<Animator>().Play("TakeHit", -1);
        lifeBar.SetLife(life);

    }

    public void MpDecrease(int value)
    {
        mana = mana - value;
        manaBar.SetMana(mana);
    }

    public void HpHeal(int value)
    {
        life = life + value;
        lifeBar.SetLife(life);
    }

    public void MpHeal(int value)
    {
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
}

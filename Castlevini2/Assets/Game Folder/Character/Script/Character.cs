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

    // Start is called before the first frame update
    void Start()
    {
        maxLife = life;
        lifeBar.setMaxLife(life);
        maxMana = mana;
        manaBar.setMaxMana(mana);
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

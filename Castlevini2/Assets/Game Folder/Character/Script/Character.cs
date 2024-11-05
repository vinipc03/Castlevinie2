using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int life;
    public int maxLife;
    public Transform skin;

    // Start is called before the first frame update
    void Start()
    {
        maxLife = life;
    }

    // Update is called once per frame
    void Update()
    {
        LifeControl();
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
}

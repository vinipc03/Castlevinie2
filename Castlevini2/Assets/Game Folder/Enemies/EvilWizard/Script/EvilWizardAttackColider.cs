using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizardAttackColider : MonoBehaviour
{
    public Transform evilWizard;
    public Transform skin;
    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Flip();
    }

    private void Flip()
    {
        if (isFacingRight && skin.localScale.x < 0 || !isFacingRight && skin.localScale.x > 0)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
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
}

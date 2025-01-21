using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlataform : MonoBehaviour
{
    private GameObject currentOneWayPlatform;

    [SerializeField] private CapsuleCollider2D playerCollider;
    [SerializeField] private CapsuleCollider2D playerCrouchCollider;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentOneWayPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform")) ;
        {
            currentOneWayPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"));
        {
            currentOneWayPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D plataformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, plataformCollider);
        Physics2D.IgnoreCollision(playerCrouchCollider, plataformCollider);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(playerCollider, plataformCollider, false);
    }
}

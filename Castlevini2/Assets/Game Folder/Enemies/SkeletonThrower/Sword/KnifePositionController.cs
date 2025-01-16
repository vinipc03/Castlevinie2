using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifePositionController : MonoBehaviour
{
    public Transform skeletonThrower;
    public Transform skin;

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
        // Verifica a escala no eixo X de 'skin' para decidir a rotação
        if (skin.localScale.x < 0 && transform.rotation.y == 0)
        {
            // Se skin está voltado para a esquerda e o objeto ainda não está rotacionado
            transform.Rotate(0f, 180f, 0f);
            this.GetComponent<Transform>().localScale = skin.localScale;
        }
        else if (skin.localScale.x > 0 && transform.rotation.y != 0)
        {
            // Se skin está voltado para a direita e o objeto está rotacionado
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

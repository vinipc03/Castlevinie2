using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordThrowController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float forceX = 150f;  // Força no eixo X
    public float forceY = 300f;  // Força no eixo Y
    public float rotationSpeed = 300f;  // Velocidade de rotação no eixo Z

    void Start()
    {
        // Aplica a força inicial no projétil
        Destroy(this.gameObject, 3f);  // Destroi o projétil após 3 segundos
    }
    private void Update()
    {
        // Rotaciona o projétil continuamente no eixo Z
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);  // Rotação contínua
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Character>().PlayerDamage(1);
            collision.GetComponent<PlayerController>().KnockBack(transform.position);
            Destroy(this.gameObject);
        }
        
    }
}

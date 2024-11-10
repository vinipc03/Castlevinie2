using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerCollider : MonoBehaviour
{
    public GameObject knifePrefab;
    public Transform knifePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Throw()
    {
        GameObject knife = Instantiate(knifePrefab, knifePosition.position, transform.rotation); // Usa a rotação do inimigo
        Rigidbody2D rbKnife = knife.GetComponent<Rigidbody2D>();

        // Se o inimigo está virado para a esquerda, aplica a força negativa, se estiver para a direita aplica a positiva.
        if (transform.localScale.x < 0)
        {
            rbKnife.AddForce(new Vector2(-150f, 300f)); // Força para a esquerda
        }
        else
        {
            rbKnife.AddForce(new Vector2(150f, 300f)); // Força para a direita
        }
        //Instantiate(knifePrefab, knifePosition.position, transform.rotation);
    }


}

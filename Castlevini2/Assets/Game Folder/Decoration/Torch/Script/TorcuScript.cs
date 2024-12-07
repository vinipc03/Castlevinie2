using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorcuScript : MonoBehaviour
{
    public Transform torchPrefab;

    [System.Serializable]
    public class DropItem
    {
        public GameObject itemPrefab; // O prefab do item
        public float dropChance;      // Chance de soltar o item
    }

    public DropItem[] possibleDrops; // Lista de itens possíveis

    public void Break()
    {
        torchPrefab.GetComponent<Animator>().Play("Broke", -1);

        // Espera o tempo da animação antes de destruir o objeto
        float animationDuration = torchPrefab.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, animationDuration);
        // Destroi o objeto
        Destroy(gameObject);
        
        // Sorteia um item para soltar
        float randomValue = Random.Range(0f, 100f);
        float cumulativeChance = 0f;

        foreach (DropItem drop in possibleDrops)
        {
            cumulativeChance += drop.dropChance;
            if (randomValue <= cumulativeChance)
            {
                // Instancia o item no local do objeto
                Instantiate(drop.itemPrefab, transform.position, Quaternion.identity);
                return; // Apenas um item será solto
            }
        }
    }

    // Exemplo de detecção de ataque
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            
            Break();
        }
    }
}

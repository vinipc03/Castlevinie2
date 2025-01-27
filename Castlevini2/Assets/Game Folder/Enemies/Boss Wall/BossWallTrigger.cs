using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWallTrigger : MonoBehaviour
{
    public GameObject wall1; // Referência à primeira parede
    public GameObject wall2; // Referência à segunda parede
    public GameObject boss;

    private void Start()
    {
        // Garante que as paredes comecem invisíveis e desativadas
        SetWallState(false);
    }
    private void Update()
    {
        if(boss.GetComponent<Character>().life <= 0)
        {
            wall1.GetComponent<Animator>().SetBool("bossBattle", false);
            wall2.GetComponent<Animator>().SetBool("bossBattle", false);
            OnBossDeath();
        }
    }
    // Método chamado quando o boss morre
    public void OnBossDeath()
    {
        StartCoroutine(DisableWallsWithDelay(2f)); // Atraso de 2 segundos
    }

    private IEnumerator DisableWallsWithDelay(float delay)
    {
        // Aguarda pelo tempo especificado
        yield return new WaitForSeconds(delay);

        // Desativa as paredes
        SetWallState(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que entrou no trigger é o jogador
        if (other.CompareTag("Player"))
        {
            // Ativa as paredes e as torna visíveis
            SetWallState(true);
            wall1.GetComponent<Animator>().SetBool("bossBattle", true);
            wall2.GetComponent<Animator>().SetBool("bossBattle", true);
        }
    }

    private void SetWallState(bool state)
    {
        // Ativa ou desativa as paredes
        wall1.SetActive(state);
        wall2.SetActive(state);

        // Torna as paredes visíveis ou invisíveis
        if (wall1.TryGetComponent<SpriteRenderer>(out SpriteRenderer renderer1))
        {
            renderer1.enabled = state;
        }
        if (wall2.TryGetComponent<SpriteRenderer>(out SpriteRenderer renderer2))
        {
            renderer2.enabled = state;
        }
    }
}

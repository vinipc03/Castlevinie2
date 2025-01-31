using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStrikeController : MonoBehaviour
{
    public Transform attackCollider;
    public GameObject lightStrikePrefab;
    public AudioSource audioSource;
    public AudioClip lightStrikeSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LightStrike()
    {
        // ATIRAR HOLY BOLT
        audioSource.PlayOneShot(lightStrikeSound, 0.5f);
        Instantiate(lightStrikePrefab, attackCollider.position, attackCollider.rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyBoltController : MonoBehaviour
{

    public Transform attackCollider;
    public GameObject holyBoltPrefab;
    public AudioSource audioSource;
    public AudioClip holyBoltSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        // ATIRAR HOLY BOLT
        audioSource.PlayOneShot(holyBoltSound, 0.2f);
        Instantiate(holyBoltPrefab, attackCollider.position, attackCollider.rotation);
    }
}

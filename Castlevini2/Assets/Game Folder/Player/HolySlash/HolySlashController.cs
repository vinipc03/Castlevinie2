using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolySlashController : MonoBehaviour
{
    public Transform attackCollider;
    public GameObject holySlashPrefab;
    public AudioSource audioSource;
    public AudioClip holySlashSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HolySlash()
    {
        // ATIRAR HOLY BOLT
        audioSource.PlayOneShot(holySlashSound, 0.5f);
        Instantiate(holySlashPrefab, attackCollider.position, attackCollider.rotation);
    }
}

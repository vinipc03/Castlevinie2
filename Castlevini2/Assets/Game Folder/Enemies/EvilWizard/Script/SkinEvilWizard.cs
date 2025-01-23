using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinEvilWizard : MonoBehaviour
{
    public Transform attackCollider;
    public GameObject fireBallPrefab;

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
        Instantiate(fireBallPrefab, attackCollider.position, attackCollider.rotation);
    }
}

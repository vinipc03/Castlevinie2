using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyExplosionController : MonoBehaviour
{

    public Transform attackCollider;
    public GameObject holyExplosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explosion()
    {
        // ATIRAR HOLY BOLT
        Instantiate(holyExplosionPrefab, attackCollider.position, attackCollider.rotation);
    }
}

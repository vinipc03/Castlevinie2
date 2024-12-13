using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStrikeController : MonoBehaviour
{
    public Transform attackCollider;
    public GameObject lightStrikePrefab;

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
        Instantiate(lightStrikePrefab, attackCollider.position, attackCollider.rotation);
    }
}

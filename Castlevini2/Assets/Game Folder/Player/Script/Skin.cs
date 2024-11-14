using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void FinishAttack()
    {
        player.GetComponent<PlayerController>().onAttack = false;
    }

    public void FinishBlock()
    {
        player.GetComponent<PlayerController>().isBloking = false;
        this.GetComponent<Animator>().SetBool("Defend", false);
    }

}

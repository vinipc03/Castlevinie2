using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMessege : MonoBehaviour
{
    public GameObject floatingText;
    public float radius;
    private bool inRadius;
    [SerializeField] LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MessegeRadius();
    }

    private void MessegeRadius()
    {
        inRadius = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        if (inRadius)
        {
            floatingText.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            floatingText.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

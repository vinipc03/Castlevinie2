using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private Renderer myRenderer;
    private Material myMaterial;

    private float offset;

    [SerializeField] private float increase;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
        myMaterial = myRenderer.material;
    }

    private void FixedUpdate()
    {
        offset += increase;
        myMaterial.SetTextureOffset("_MainTex", new Vector2((offset * speed) / 1000, 0));
    }
}

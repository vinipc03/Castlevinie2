using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float length;
    private float StartPos;

    private Transform cam;

    public float parallaxEffect;

    private void Start()
    {
        StartPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        cam = Camera.main.transform;
    }

    private void Update()
    {
        float RePos = cam.transform.position.x * (1 - parallaxEffect);
        float Distance = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(StartPos + Distance, transform.position.y, transform.position.z);

        if (RePos > StartPos + length)
        {
            StartPos += length;
        }
        else if (RePos < StartPos - length)
        {
            StartPos -= length;
        }

    }
}

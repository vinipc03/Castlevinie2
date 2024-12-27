using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<CanvasGroup>().alpha += Time.deltaTime * 0.5f;
    }
}

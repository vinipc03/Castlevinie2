using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public Transform playerController;
    private void OnEnable()
    {
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Time.timeScale = 0f;
        playerController.GetComponent<PlayerController>().isPaused = true;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        playerController.GetComponent<PlayerController>().isPaused = false;
    }
}

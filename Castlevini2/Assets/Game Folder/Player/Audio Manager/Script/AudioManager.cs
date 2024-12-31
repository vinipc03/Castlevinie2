using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Transform player;

    public AudioSource audioSource;
    public AudioClip mainMenuMusic;
    public AudioClip level1music;
    public AudioClip level2music;

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MusicManager();
    }

    public void MusicManager()
    {
        if (player == null)
        {
            audioSource.PlayOneShot(mainMenuMusic, 0.5f);
        }

        if(player != null)
        {
            if (player.GetComponent<PlayerController>().currentLevel == "Level1")
            {
                audioSource.PlayOneShot(level1music, 0.5f);
            }

            if (player.GetComponent<PlayerController>().currentLevel == "Level2")
            {
                audioSource.PlayOneShot(level2music, 0.5f);
            }
        }
        
    }
}

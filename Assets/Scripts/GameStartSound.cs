using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartSound : MonoBehaviour
{
    public AudioClip mySoundClip;// Start is called before the first frame update
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = mySoundClip;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

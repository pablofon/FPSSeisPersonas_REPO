using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeScript : MonoBehaviour
{
  
    [SerializeField] private AudioClip audioTape;
    AudioSource myAudio;
    bool alreadyPlayed;

    private void Awake()
    {
       myAudio = GetComponent<AudioSource>();
        alreadyPlayed = false;
    }

    public void PlayTape()
    {
        if (!alreadyPlayed)
        {
            myAudio.clip = audioTape;
            myAudio.Play();
            alreadyPlayed = true;
        }
        
    }
}

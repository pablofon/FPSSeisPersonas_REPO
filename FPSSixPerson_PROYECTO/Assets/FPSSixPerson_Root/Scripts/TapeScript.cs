using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeScript : MonoBehaviour
{
  
    [SerializeField] private AudioClip audioTape;
    AudioSource myAudio;

    private void Awake()
    {
       myAudio = GetComponent<AudioSource>();
    }

    public void PlayTape()
    {
        myAudio.clip = audioTape;
        myAudio.Play();
    }
}

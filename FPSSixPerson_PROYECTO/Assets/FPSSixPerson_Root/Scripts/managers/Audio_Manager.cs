using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    
    
    [Header("_________Audio Source__________")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("_________Clips_________")]
    public AudioClip background;
    public AudioClip PickUp;
    public AudioClip PickUp2;
    public AudioClip Detected;
    public AudioClip Door;
    public AudioClip NoDoor;
    public AudioClip Light;
    public AudioClip Panel;
    public AudioClip Buttons;
    public AudioClip Incorrect;
    public AudioClip correct;
    public AudioClip Weapon;
    public AudioClip Vent;




    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
        
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}

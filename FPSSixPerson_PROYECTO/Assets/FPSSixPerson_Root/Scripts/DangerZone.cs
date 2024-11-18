using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DangerZone : MonoBehaviour
{
    [SerializeField] bool DangerAudioAlreadyPlayed;
    Audio_Manager audiomanager;
    // Start is called before the first frame update0

    private void Awake()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio_Manager>();
        DangerAudioAlreadyPlayed = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("peligro");

            audiomanager.PlaySFX(audiomanager.Danger);
            DangerAudioAlreadyPlayed = true;

        }
    }

   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorInteractor : MonoBehaviour
{

    Animator anim;
    [SerializeField] GameObject navObstacle;
    [SerializeField] GameObject coll;
    Audio_Manager audiomanager;
    [SerializeField] bool DoorAudioAlreadyPlayed;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Awake()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio_Manager>();
        DoorAudioAlreadyPlayed = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlocked()
    {
        if (GameManager.Instance.firstKey)
        {
            anim.SetBool("Open", true);
            audiomanager.PlaySFX(audiomanager.Door);
            GameManager.Instance.firstKey = false;
            navObstacle.SetActive(false);
            coll.SetActive(false);
        }
        else
        {
            if (!DoorAudioAlreadyPlayed)
            {
                audiomanager.PlaySFX(audiomanager.NoDoor);
                DoorAudioAlreadyPlayed = true;
            }
            Debug.Log("No tienes la llave");
        }
        
    }
}

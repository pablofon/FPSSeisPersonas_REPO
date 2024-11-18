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
    [SerializeField] GameObject text;
    float textTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        text.SetActive(false);
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
            text.SetActive(true);
            Invoke(nameof(ResetText), textTime);
            if (!DoorAudioAlreadyPlayed)
            {
                audiomanager.PlaySFX(audiomanager.NoDoor);
                DoorAudioAlreadyPlayed = true;
            }
            Debug.Log("No tienes la llave");
           
        }
        
    }

    void ResetText()
    {
        text.SetActive(false);
    }
}

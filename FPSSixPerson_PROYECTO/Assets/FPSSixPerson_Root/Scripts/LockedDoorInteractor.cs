using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorInteractor : MonoBehaviour
{

    Animator anim;
    [SerializeField] GameObject navObstacle;
    [SerializeField] GameObject coll;
    Audio_Manager audiomanager;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Awake()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio_Manager>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlocked()
    {
        if (GameManager.Instance.firstKey)
        {
            anim.SetBool("open", true);
            audiomanager.PlaySFX(audiomanager.Door);
            GameManager.Instance.firstKey = false;
            navObstacle.SetActive(false);
            coll.SetActive(false);
        }
        else
        {
            audiomanager.PlaySFX(audiomanager.NoDoor);
            Debug.Log("No tienes la llave");
        }
        
    }
}

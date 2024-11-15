using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractor : MonoBehaviour
{

    Animator anim;
    [SerializeField] bool open = false;
    [SerializeField] GameObject coll;
    Collider collid;
    Audio_Manager audiomanager;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        collid = GetComponent<Collider>();
    }

    private void Awake()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        Opened();
    }

    public void Opened()
    {
        if (open)
        {
            anim.SetBool("Open", true);
            coll.SetActive(false);
        }
        if (!open)
        {
            anim.SetBool("Open", false);
            coll.SetActive(true);
        }
    }

    public void Open()
    {
        if (!open)
        {
            audiomanager.PlaySFX(audiomanager.Vent);
            open = true;
        }
        else
        {
            open = false;
        }
        //open = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            open = true;
        }
    }
}

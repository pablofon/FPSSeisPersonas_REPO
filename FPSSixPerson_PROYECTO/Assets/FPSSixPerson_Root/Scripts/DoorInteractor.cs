using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractor : MonoBehaviour
{

    Animator anim;
    [SerializeField] bool open = false;
    [SerializeField] GameObject coll;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
            anim.SetBool("open", true);
            coll.SetActive(false);
        }
        if (!open)
        {
            anim.SetBool("open", false);
            coll.SetActive(true);
        }
    }

    public void Open()
    {
        if (!open)
        {
            open = true;
        }
        else
        {
            open = false;
        }
        //open = true;
    }
}

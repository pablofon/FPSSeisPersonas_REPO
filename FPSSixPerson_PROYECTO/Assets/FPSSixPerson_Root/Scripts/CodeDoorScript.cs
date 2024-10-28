using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeDoorScript : MonoBehaviour
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
        
    }

    public void Interacted()
    {
        
    }
}

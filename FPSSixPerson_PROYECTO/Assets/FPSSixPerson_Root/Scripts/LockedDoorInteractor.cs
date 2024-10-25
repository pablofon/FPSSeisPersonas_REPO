using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorInteractor : MonoBehaviour
{

    Animator anim;
    [SerializeField] GameObject navObstacle;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlocked()
    {
        anim.SetBool("open", true);
        GameManager.Instance.firstKey = false;
        navObstacle.SetActive(false);
    }
}

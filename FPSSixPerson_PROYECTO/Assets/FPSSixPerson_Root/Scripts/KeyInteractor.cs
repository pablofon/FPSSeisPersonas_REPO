using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteractor : MonoBehaviour
{
    Audio_Manager audiomanager;
    private void Awake()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio_Manager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Grabbed()
    {
        audiomanager.PlaySFX(audiomanager.PickUp);
        GameManager.Instance.firstKey = true;
        gameObject.SetActive(false);
    }
}

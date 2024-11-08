using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossScript : MonoBehaviour
{
    Audio_Manager audiomanager;

    [SerializeField] float respawnTime = 30f;
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
        gameObject.SetActive(false);
        Invoke(nameof(RespawnCross), respawnTime);
    }

    void RespawnCross()
    {
        gameObject.SetActive(true);
    }
}

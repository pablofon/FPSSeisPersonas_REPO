using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperInteractor : MonoBehaviour
{
    [SerializeField] GameObject panel;
    Audio_Manager audiomanager;

    private void Awake()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio_Manager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LookPaper()
    {
        audiomanager.PlaySFX(audiomanager.Paper);
        panel.SetActive(true);
    }

    public void StopLooking()
    {
        audiomanager.PlaySFX(audiomanager.Paper);
        panel.SetActive(false);
        GameManager.Instance.lookingPaper = false;
    }
}

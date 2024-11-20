using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperInteractor : MonoBehaviour
{
    [SerializeField] GameObject panel;
    Audio_Manager audiomanager;

    
    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.lookingPaper)
        {
            StopLooking();
        }
    }

    public void LookPaper()
    {
        
        panel.SetActive(true);
    }

    public void StopLooking()
    {
        
        panel.SetActive(false);
        GameManager.Instance.lookingPaper = false;
    }
}

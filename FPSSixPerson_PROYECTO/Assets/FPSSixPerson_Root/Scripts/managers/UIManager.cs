using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("General UI References")]
    [SerializeField] GameObject keyPannel;

    // Start is called before the first frame update
    void Start()
    {
        keyPannel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.firstKey)
        {
            keyPannel.SetActive(true);
        }
    }
}

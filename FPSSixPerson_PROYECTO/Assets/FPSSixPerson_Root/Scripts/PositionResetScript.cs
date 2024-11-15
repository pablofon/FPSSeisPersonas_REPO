using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PositionResetScript : MonoBehaviour
{
    [SerializeField] int sceneReseter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Caido");
            SceneManager.LoadScene(sceneReseter);

        }
    }
}

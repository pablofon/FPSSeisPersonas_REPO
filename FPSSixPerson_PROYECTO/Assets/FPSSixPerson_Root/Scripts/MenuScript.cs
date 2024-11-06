using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Retry()
    {
        SceneManager.LoadScene(2);
    }

    public void Retrurn()
    {
        SceneManager.LoadScene(0);
    }
}


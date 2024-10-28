using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyPadScript : MonoBehaviour
{
    [SerializeField] TMP_Text ans;
    [SerializeField] string answer;

    public void Number(int number)
    {
        ans.text += number.ToString();
    }

    public void EnterNumber()
    {
        if (ans.text == answer)
        {
            ans.text = "Correct";
        }
        else
        {
            ans.text = "Incorrect";
        }
    }

    public void ClosePad()
    {
        
    }
}
